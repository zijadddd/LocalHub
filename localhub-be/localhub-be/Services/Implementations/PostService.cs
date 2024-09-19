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

        PostOut response = new PostOut(post.Id, post.Name, post.Description, post.PhotoUrl, 0, 0, post.Created, post.Updated);

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

    public async Task<UserLikedPostOut> DidUserLikedPost(Guid userId, Guid postId) {
        User user = await _databaseContext.Users.Include(user => user.Likes).FirstOrDefaultAsync(user => user.Id.Equals(userId));
        if (user is null) throw new UserNotFoundException(userId);

        Post post = await _databaseContext.Posts.FirstOrDefaultAsync(post => post.Id.Equals(postId));
        if (post is null) throw new PostNotFoundException(postId);

        Like like = user.Likes.FirstOrDefault(like => like.PostId.Equals(post.Id));
        if (like is null) return new UserLikedPostOut(false);

        return new UserLikedPostOut(true);
    }

    public async Task<PostOut> Get(Guid id) {
        Post post = await _databaseContext.Posts.Include(post => post.Likes).Include(post => post.Comments).Include(post => post.User).FirstOrDefaultAsync(post => post.Id.Equals(id));
        if (post is null) throw new PostNotFoundException(id);

        PostOut response = new PostOut(post.Id, post.Name, post.Description, post.PhotoUrl, post.User.FirstName + ' ' + post.User.LastName, post.Likes.Count, post.Comments.Count, post.Created, post.Updated);

        return response;
    }

    public async Task<List<PostOut>> GetAll() {
        List<Post> posts = await _databaseContext.Posts.Include(post => post.Likes).Include(post => post.Comments).ToListAsync();
        if (posts.IsNullOrEmpty()) throw new NoPostsAvailableException();

        List<PostOut> response = posts.Select(post => new PostOut(
            post.Id, post.Name, post.Description, post.PhotoUrl, post.Likes.Count, post.Comments.Count, post.Created, post.Updated
        )).ToList();

        return response;
    }

    public async Task<LikeAndCommentCountOut> GetUserLikeAndCommentCount(Guid userId) {
        User user = await _databaseContext.Users.Include(user => user.Likes).Include(user => user.Comments).FirstOrDefaultAsync(user => user.Id.Equals(userId));
        if (user is null) throw new UserNotFoundException(userId);

        LikeAndCommentCountOut response = new LikeAndCommentCountOut(user.Likes.Count, user.Comments.Count);

        return response;
    }

    public async Task<LikeOut> LikePost(Guid userId, Guid postId) {
        User user = await _databaseContext.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
        if (user is null) throw new UserNotFoundException(userId);

        Post post = await _databaseContext.Posts.Include(post => post.Likes).FirstOrDefaultAsync(post => post.Id.Equals(postId));
        if (post is null) throw new PostNotFoundException(postId);

        Like like = new Like { UserId = userId, PostId = postId };
        _databaseContext.Likes.Add(like);
        await _databaseContext.SaveChangesAsync();

        LikeOut response = new LikeOut(post.Likes.Count + 1);

        return response;
    }

    public async Task<PostOut> Update(Guid id, PostIn request) {
        Post post = await _databaseContext.Posts.Include(post => post.Likes.Count).Include(post => post.Comments).FirstOrDefaultAsync(post => post.Id.Equals(id));
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

        PostOut response = new PostOut(post.Id, post.Name, post.Description, post.PhotoUrl, post.Likes.Count, post.Comments.Count, post.Created, post.Updated);

        return response;
    }
}
