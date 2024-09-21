namespace localhub_be.Models.DTOs;
public sealed record CommentOut {
    public Guid Id { get; init; }
    public string? Content { get; init; }
    public UserOut User { get; init; }
    public Guid PostId { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }

    public CommentOut(Guid id, string? content, UserOut user, Guid postId, DateTime created, DateTime updated) {
        Id = id;
        Content = content;
        User = user;
        PostId = postId;
        Created = created;
        Updated = updated;
    }
}
