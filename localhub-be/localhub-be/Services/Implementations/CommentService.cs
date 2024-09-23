using localhub_be.Core;
using localhub_be.Core.Exceptions;
using localhub_be.Models.DAOs;
using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace localhub_be.Services.Implementations;
public sealed class CommentService : ICommentService {

    private readonly DatabaseContext _databaseContext;

    public CommentService(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    public async Task<CommentOut> Create(Guid postId, Guid userId, CommentIn request) {
        User user = await _databaseContext.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
        if (user is null) throw new UserNotFoundException(userId);

        Post post = await _databaseContext.Posts.FirstOrDefaultAsync(post => post.Id.Equals(postId));
        if (post is null) throw new PostNotFoundException(postId);

        Comment comment = new Comment {
            Content = request.Content,
            UserId = user.Id,
            PostId = post.Id,
        };

        _databaseContext.Comments.Add(comment);
        await _databaseContext.SaveChangesAsync();

        UserOut userOut = new UserOut(
            user.Id,
            user.FirstName,
            user.LastName,
            user.BirthDate,
            user.Address,
            user.Region,
            user.PhoneNumber,
            user.MembershipDate,
            user.Created,
            user.Updated,
            user.Auth?.Email,
            user.ProfilePhotoUrl
        );

        CommentOut response = new CommentOut(comment.Id, comment.Content, userOut, comment.PostId, comment.Created, comment.Updated);

        return response;
    }

    public async Task<MessageOut> Delete(Guid commentId) {
        Comment comment = await _databaseContext.Comments.FirstOrDefaultAsync(comment => comment.Id.Equals(commentId));
        if (comment is null) throw new CommentNotFoundException(commentId);

        _databaseContext.Comments.Remove(comment);
        await _databaseContext.SaveChangesAsync();

        return new MessageOut($"Your comment has been successfully deleted.");
    }

    public async Task<List<CommentOut>> GetAll(Guid postId) {
        List<Comment> comments = await _databaseContext.Comments.Include(comment => comment.User).Where(comment => comment.PostId.Equals(postId)).ToListAsync();

        List<CommentOut> response = comments.Select(comment => new CommentOut(
            comment.Id, comment.Content, new UserOut(
                comment.User.Id, comment.User.FirstName, comment.User.LastName, comment.User.BirthDate, comment.User.Address, comment.User.Region,
                comment.User.PhoneNumber, comment.User.MembershipDate, comment.User.Created, comment.User.Updated, comment.User.Auth?.Email, 
                comment.User.ProfilePhotoUrl
            ), comment.PostId, comment.Created, comment.Updated
        )).ToList();

        if (response.IsNullOrEmpty()) throw new NoCommentsAvailableForPostException(postId);

        return response;
    }

    public async Task<CommentOut> Update(Guid commentId, CommentUpdateIn request) {
        Comment comment = await _databaseContext.Comments.Include(comment => comment.User).FirstOrDefaultAsync(comment => comment.Id.Equals(commentId));
        if (comment is null) throw new CommentNotFoundException(commentId);

        comment.Content = request.Content;
        comment.Updated = DateTime.Now;

        _databaseContext.Comments.Update(comment);
        await _databaseContext.SaveChangesAsync();

        CommentOut response = new CommentOut(
                comment.Id, comment.Content, new UserOut(
                comment.User.Id, comment.User.FirstName, comment.User.LastName, comment.User.BirthDate, comment.User.Address, comment.User.Region,
                comment.User.PhoneNumber, comment.User.MembershipDate, comment.User.Created, comment.User.Updated, comment.User.Auth?.Email,
                comment.User.ProfilePhotoUrl
            ), comment.PostId, comment.Created, comment.Updated);

        return response;
    }
}
