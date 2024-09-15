namespace localhub_be.Models.DTOs;
public sealed record PostOut {
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
    public DateTime? Created { get; init; }
    public DateTime? Updated { get; init; }

    public PostOut(Guid id, string? name, string? description, string? imageUrl, DateTime? created, DateTime? updated) {
        Id = id;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Created = created;
        Updated = updated;
    }
}
