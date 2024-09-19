namespace localhub_be.Models.DTOs;
public sealed record PostOut {
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
    public string? UserName { get; init; }
    public int Likes { get; init; }
    public int Comments { get; init; }
    public DateTime? Created { get; init; }
    public DateTime? Updated { get; init; }

    public PostOut(Guid id, string? name, string? description, string? imageUrl, string? userName, int likes, int comments, DateTime? created, DateTime? updated) {
        Id = id;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        UserName = userName;
        Likes = likes;
        Comments = comments;
        Created = created;
        Updated = updated;
    }
}
