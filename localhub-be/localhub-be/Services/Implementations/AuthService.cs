using localhub_be.Core;
using localhub_be.Core.Exceptions;
using localhub_be.Models.DAOs;
using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace localhub_be.Services.Implementations;
public sealed class AuthService : IAuthService {

    private readonly DatabaseContext _databaseContext;
    private readonly IConfiguration _configuration;

    public AuthService(DatabaseContext databaseContext, IConfiguration configuration) {
        _databaseContext = databaseContext;
        _configuration = configuration;
    }

    public async Task<AuthOut> AuthenticateUser(AuthIn request) {
        Auth? userAuthInfo = await _databaseContext.Auths.Include(uai => uai.Role).Include(uai => uai.User).FirstOrDefaultAsync(user => user.Email == request.Email);

        if (userAuthInfo is null) throw new UserAuthInfoNotFoundException(request.Email!);
        if (!BCrypt.Net.BCrypt.Verify(request.Password, userAuthInfo.Password)) throw new IncorrectPasswordException();

        return new AuthOut(CreateTokenAsync(userAuthInfo));
    }

    private string CreateTokenAsync(Auth userAuthInfo) {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Email, userAuthInfo.Email!),
            new Claim(ClaimTypes.Role, userAuthInfo?.Role?.Name!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Secret")!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
