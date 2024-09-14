namespace localhub_be.Models.DAOs;
public sealed class User {
    public Guid Id { get; set; }
    public string? FirstName { get; set; } 
    public string? LastName { get; set; } 
    public DateTime BirthDate { get; set; }
    public string? Address { get; set; }
    public string? Region { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime MembershipDate { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    public Auth? Auth { get; set; }
    public string? ProfilePhotoUrl { get; set; }
}
