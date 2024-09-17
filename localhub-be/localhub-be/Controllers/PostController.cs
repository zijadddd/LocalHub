using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace localhub_be.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class PostController : BaseController {

    private readonly IPostService _postService;

    public PostController(IPostService postService) {
        _postService = postService;
    }

    [HttpGet, Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<List<PostOut>>> GetAllPosts() {
        List<PostOut> response = await _postService.GetAll();

        return Ok(response);
    }

    [HttpGet("{id}"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<PostOut>> GetPost(Guid id) {
        PostOut response = await _postService.Get(id);

        return Ok(response);
    }

    [HttpPost, Authorize(Roles = "Administrator")]
    public async Task<ActionResult<PostOut>> CreatePost(PostIn request) {
        ValidateModelState();
        PostOut response = await _postService.Create(request);

        return Ok(response);
    }

    [HttpDelete, Authorize(Roles = "Administrator")]
    public async Task<ActionResult<MessageOut>> DeletePost(Guid id) {
        MessageOut response = await _postService.Delete(id);

        return Ok(response);
    }

    [HttpPut("{id}"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<PostOut>> UpdatePost(Guid id, PostIn request) {
        ValidateModelState();
        PostOut response = await _postService.Update(id, request);

        return Ok(response);
    }

    [HttpPost("{userId}/{postId}/likePost"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<LikeOut>> LikePost(Guid userId, Guid postId) {
        LikeOut response = await _postService.LikePost(userId, postId);

        return response;
    }

    [HttpGet("{userId}/getLikeAndCommentCount"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<LikeAndCommentCountOut>> GetUserLikeAndCommentCount(Guid userId) {
        LikeAndCommentCountOut response = await _postService.GetUserLikeAndCommentCount(userId);

        return response;
    }
}

