namespace localhub_be.Models.DAOs;
public sealed class Auth {
    public Guid Id { get; set; }
    public string? Email { get; set; } 
    public string? Password { get; set; } 
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } 
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
}

