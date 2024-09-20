using localhub_be.Models.DTOs;

namespace localhub_be.Services.Interfaces;
public interface IPostService : IGeneric<PostIn, PostOut> {
    Task<PostOut> Create(Guid userId, PostIn request);
    Task<LikeOut> LikePost(Guid userId, Guid postId);
    Task<LikeAndCommentCountOut> GetUserLikeAndCommentCount(Guid userId);
    Task<UserLikedPostOut> DidUserLikedPost(Guid userId, Guid postId);
}
