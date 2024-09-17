using localhub_be.Models.DTOs;

namespace localhub_be.Services.Interfaces;
public interface IPostService : IGeneric<PostIn, PostOut> {
    Task<LikeOut> LikePost(Guid userId, Guid postId);
    Task<LikeAndCommentCountOut> GetUserLikeAndCommentCount(Guid userId);
}
