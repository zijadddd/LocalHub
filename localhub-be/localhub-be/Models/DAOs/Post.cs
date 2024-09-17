namespace localhub_be.Models.DAOs;
public sealed class Post {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } 
    public List<Comment> Comments { get; } = [];
    public List<Like> Likes { get; } = [];
}
