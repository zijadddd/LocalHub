namespace localhub_be.Models.DTOs;
public sealed record CommentOut {
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    public CommentOut(Guid id, string? content, Guid userId, Guid postId, DateTime created, DateTime updated) {
        Id = id;
        Content = content;
        UserId = userId;
        PostId = postId;
        Created = created;
        Updated = updated;
    }
}
