﻿using localhub_be.Core;
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

    public async Task<CommentOut> Create(Guid postId, CommentIn request) {
        Post post = await _databaseContext.Posts.FirstOrDefaultAsync(post => post.Id.Equals(postId));
        if (post is null) throw new PostNotFoundException(postId);

        Comment comment = new Comment {
            Content = request.Content,
            UserId = request.UserId,
            PostId = postId,
        };

        _databaseContext.Comments.Add(comment);
        await _databaseContext.SaveChangesAsync();

        CommentOut response = new CommentOut(comment.Id, comment.Content, comment.UserId, comment.PostId, comment.Created, comment.Updated);

        return response;
    }

    public async Task<MessageOut> Delete(Guid commentId) {
        Comment comment = await _databaseContext.Comments.FirstOrDefaultAsync(comment => comment.Id.Equals(commentId));
        if (comment is null) throw new CommentNotFoundException(commentId);

        _databaseContext.Comments.Remove(comment);
        await _databaseContext.SaveChangesAsync();

        return new MessageOut($"Comment with id {commentId} was successfully deleted.");
    }

    public async Task<List<CommentOut>> GetAll(Guid postId) {
        Post post = await _databaseContext.Posts.Include(post => post.Comments).FirstOrDefaultAsync(post => post.Id.Equals(postId));
        if (post is null) throw new PostNotFoundException(postId);

        List<CommentOut> response = post.Comments.Select(comment => new CommentOut(
            comment.Id, comment.Content, comment.UserId, comment.PostId, comment.Created, comment.Updated
        )).ToList();

        if (response.IsNullOrEmpty()) throw new NoCommentsAvailableForPostException(postId);

        return response;
    }

    public async Task<CommentOut> Update(Guid commentId, CommentUpdateIn request) {
        Comment comment = await _databaseContext.Comments.FirstOrDefaultAsync(comment => comment.Id.Equals(commentId));
        if (comment is null) throw new CommentNotFoundException(commentId);

        comment.Content = request.Content;
        comment.Updated = DateTime.Now;

        _databaseContext.Comments.Update(comment);
        await _databaseContext.SaveChangesAsync();

        CommentOut response = new CommentOut(comment.Id, comment.Content, comment.UserId, comment.PostId, comment.Created, comment.Updated);

        return response;
    }
}
