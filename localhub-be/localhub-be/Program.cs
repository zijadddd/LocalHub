using localhub_be.Core;
using Microsoft.OpenApi.Models;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using localhub_be.Services.Interfaces;
using localhub_be.Services.Implementations;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using localhub_be.Models.DTOs;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString");

if (string.IsNullOrEmpty(connectionString)) 
    throw new Exception("Connection string not found. Ensure the .env file is correctly configured and placed in the root directory.");

builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?> {
    { "ConnectionStrings:Database", connectionString }
});

builder.Services.AddDbContext<DatabaseContext>(options => {
    var databaseVersion = builder.Configuration["DatabaseVersion"];
    options.UseMySql(connectionString, new MySqlServerVersion(databaseVersion));
});

var secret = Environment.GetEnvironmentVariable("Secret");

if (string.IsNullOrEmpty(secret))
    throw new Exception("Secret not found. Ensure the .env file is correctly configured and placed in the root directory.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddCors(options => {
    options.AddPolicy(name: "AuthenticationPolicy",
                  policy => {
                      policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                  });
    options.AddDefaultPolicy(
                  policy => {
                      policy.AllowAnyOrigin().WithHeaders("Authorization").AllowAnyMethod();
                  });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "LocalHub_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});

// The configuration `SuppressModelStateInvalidFilter = true` disables the automatic inclusion of `ModelState`
// errors in the response, such as a `400 Bad Request` for invalid models. Instead, you need to manually handle errors
// in the controller by checking `ModelState.IsValid` and returning an appropriate response.
// That is default case.
// When the model is not valid, the error is caught on line 108 in this file and returned to the user as a MessageOut object.

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global exception handler
app.UseExceptionHandler(options => {
    options.Run(async context => {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>();

        if (exception != null) {
            var responses = exception.Error.Message.Split(";", StringSplitOptions.RemoveEmptyEntries);

            var errorResponse = responses.Select(response => new MessageOut(response)).ToList();
            var jsonResponse = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(jsonResponse).ConfigureAwait(false);
        }
    });
});

app.UseStaticFiles(new StaticFileOptions {
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/Uploads"
});

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
