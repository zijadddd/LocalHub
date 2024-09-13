using localhub_be.Core;
using localhub_be.Core.Exceptions;
using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using System;

namespace localhub_be.Services.Implementations;
public sealed class FileService : IFileService {
    IWebHostEnvironment _environment; 
    IHttpContextAccessor _httpContextAccessor;
    DatabaseContext _databaseContext;

    private readonly string[] allowedFileExtensions = [".jpg", ".jpeg", ".png"];

    public FileService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor, DatabaseContext databaseContext) {
        _environment = environment;
        _httpContextAccessor = httpContextAccessor;
        _databaseContext = databaseContext;
    }

    public async Task<PictureOut> SaveFileAsync(PictureIn request) {
        if (request is null || request.Image is null) throw new ImageNotProvidedException();
        if (request.Image.Length > 200 * 1024) throw new ImageSizeExceededException();


        string contentPath = _environment.ContentRootPath;
        string path = Path.Combine(contentPath, "Uploads");

        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        string ext = Path.GetExtension(request.Image.FileName);
        if (!allowedFileExtensions.Contains(ext)) throw new InvalidFileExtensionException();

        HttpRequest url = _httpContextAccessor.HttpContext.Request;
        string fileName = $"{Guid.NewGuid().ToString()}{ext}";
        string fileNameWithPath = Path.Combine(path, fileName);

        using var stream = new FileStream(fileNameWithPath, FileMode.Create);
        await request.Image.CopyToAsync(stream);

        string baseUrl = $"{url.Scheme}://{url.Host}/Uploads/";
        string fileUrl = $"{baseUrl}{fileName}";

        return new PictureOut(fileUrl);
    }

    public MessageOut DeleteFile(string imageName) {
        if (string.IsNullOrEmpty(imageName)) throw new ImageNotProvidedException();
        
        var contentPath = _environment.ContentRootPath;
        var path = Path.Combine(contentPath, $"Uploads", imageName);

        if (!File.Exists(path)) throw new FileNotFoundException($"Invalid file path");

        File.Delete(path);
        
        return new MessageOut("Photo successfully deleted.");
    }
}
