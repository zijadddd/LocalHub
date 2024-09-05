namespace localhub_be.Models.DAOs;
public sealed class User {
    public int Id { get; set; }
    public string? FirstName { get; set; } 
    public string? LastName { get; set; } 
    public DateOnly BirthDate { get; set; }
    public string? Address { get; set; }
    public string? Region { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly MembershipDate { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    public Auth? Auth { get; set; }
}
