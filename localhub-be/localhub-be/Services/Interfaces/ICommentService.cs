using localhub_be.Models.DTOs;

namespace localhub_be.Services.Interfaces;
public interface ICommentService {
    Task<CommentOut> Create(Guid postId, CommentIn request);
    Task<MessageOut> Delete(Guid commentId);
    Task<List<CommentOut>> GetAll(Guid postId);
    Task<CommentOut> Update(Guid commentId, CommentUpdateIn request);
}
