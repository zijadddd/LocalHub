using localhub_be.Core.Exceptions;
using localhub_be.Core;
using localhub_be.Models.DAOs;
using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace localhub_be.Services.Implementations;
public sealed class PostService : IPostService {

    private readonly DatabaseContext _databaseContext;
    private readonly IFileService _fileService;

    public PostService(DatabaseContext databaseContext, IFileService fileService) {
        _databaseContext = databaseContext;
        _fileService = fileService;
    }

    public async Task<PostOut> Create(PostIn request) {
        PictureOut pictureUrl = await _fileService.SaveFile(new PictureIn(request.Image));

        Post post = new Post {
            Name = request.Name,
            Description = request.Description,
            PhotoUrl = pictureUrl.FilePath
        };

        _databaseContext.Posts.Add(post);
        await _databaseContext.SaveChangesAsync();

        PostOut response = new PostOut(post.Id, post.Name, post.Description, post.PhotoUrl, post.Created, post.Updated);

        return response;
    }

    public async Task<MessageOut> Delete(Guid id) {
        Post post = await _databaseContext.Posts.Include(post => post.Comments).FirstOrDefaultAsync(post => post.Id.Equals(id));
        if (post is null) throw new PostNotFoundException(id);

        string uploadsPrefix = "/Uploads/";
        int index = post.PhotoUrl.IndexOf(uploadsPrefix);

        if (index == -1) throw new InvalidImageNameException();

        string result = post.PhotoUrl.Substring(index + uploadsPrefix.Length);

        _fileService.DeleteFile(result);

        _databaseContext.Remove(post);
        await _databaseContext.SaveChangesAsync();

        return new MessageOut($"Post with id {id} was successfully deleted.");
    }

    public async Task<PostOut> Get(Guid id) {
        Post post = await _databaseContext.Posts.FirstOrDefaultAsync(post => post.Id.Equals(id));
        if (post is null) throw new PostNotFoundException(id);

        PostOut response = new PostOut(post.Id, post.Name, post.Description, post.PhotoUrl, post.Created, post.Updated);

        return response;
    }

    public async Task<List<PostOut>> GetAll() {
        List<Post> posts = await _databaseContext.Posts.Include(post => post.Comments).ToListAsync();
        if (posts.IsNullOrEmpty()) throw new NoPostsAvailableException();

        List<PostOut> response = posts.Select(post => new PostOut(
            post.Id, post.Name, post.Description, post.PhotoUrl, post.Created, post.Updated
        )).ToList();

        return response;
    }

    public async Task<PostOut> Update(Guid id, PostIn request) {
        Post post = await _databaseContext.Posts.Include(post => post.Comments).FirstOrDefaultAsync(post => post.Id.Equals(id));
        if (post is null) throw new PostNotFoundException(id);

        post.Name = request.Name;
        post.Description = request.Description;
        
        if (post.PhotoUrl is not null && request.Image is not null) {
            string uploadsPrefix = "/Uploads/";
            int index = post.PhotoUrl.IndexOf(uploadsPrefix);

            if (index == -1) throw new InvalidImageNameException();

            string result = post.PhotoUrl.Substring(index + uploadsPrefix.Length);

            _fileService.DeleteFile(result);

            PictureOut filePath = await _fileService.SaveFile(new PictureIn(request.Image));
            post.PhotoUrl = filePath.FilePath;
        }

        if (post.PhotoUrl is null && request.Image is not null) {
            PictureOut filePath = await _fileService.SaveFile(new PictureIn(request.Image));
            post.PhotoUrl = filePath.FilePath;
        }

        post.Updated = DateTime.Now;

        _databaseContext.Posts.Update(post);
        await _databaseContext.SaveChangesAsync();

        PostOut response = new PostOut(post.Id, post.Name, post.Description, post.PhotoUrl, post.Created, post.Updated);

        return response;
    }
}
