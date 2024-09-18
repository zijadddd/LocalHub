using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace localhub_be.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class CommentController : BaseController {

    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService) {
        _commentService = commentService;
    }

    [HttpPost("{postId}/{userId}"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<CommentOut>> CreateComment(Guid postId, Guid userId, CommentIn request) {
        ValidateModelState();
        CommentOut response = await _commentService.Create(postId, userId, request);

        return response;
    }

    [HttpDelete("{commentId}"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<MessageOut>> DeleteComment(Guid commentId) {
        MessageOut response = await _commentService.Delete(commentId);

        return response;
    }

    [HttpGet("{postId}"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<List<CommentOut>>> GetComments(Guid postId) {
        List<CommentOut> response = await _commentService.GetAll(postId);

        return response;
    }

    [HttpPut("{commentId}"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<CommentOut>> UpdateComment(Guid commentId, CommentUpdateIn request) {
        ValidateModelState();
        CommentOut response = await _commentService.Update(commentId, request);

        return response;
    }
}

