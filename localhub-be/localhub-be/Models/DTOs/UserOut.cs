namespace localhub_be.Models.DTOs;
public sealed record UserOut {
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
    public string? Email { get; set; }

    public UserOut(Guid id, string? firstName, string? lastName, DateTime birthDate, string? address, string? region, string? phoneNumber, DateTime membershipDate, DateTime created, DateTime updated, string? email) {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Address = address;
        Region = region;
        PhoneNumber = phoneNumber;
        MembershipDate = membershipDate;
        Created = created;
        Updated = updated;
        Email = email;
    }
}
