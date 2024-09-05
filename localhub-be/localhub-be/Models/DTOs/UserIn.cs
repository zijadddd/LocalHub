namespace localhub_be.Models.DTOs;
public sealed record UserIn {
    public string? FirstName { get; init; } 
    public string? LastName { get; init; } 
    public DateOnly BirthDate { get; init; }
    public string? Address { get; init; } 
    public string? Region { get; init; }
    public string? PhoneNumber { get; init; }
    public DateOnly MembershipDate { get; init; }
    public string? Email { get; init; } 
    public string? Password { get; init; }

    public UserIn(string? firstName, string? lastName, DateOnly birthDate, string? address, string? region, string? phoneNumber, DateOnly membershipDate, string? email, string? password) {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Address = address;
        Region = region;
        PhoneNumber = phoneNumber;
        MembershipDate = membershipDate;
        Email = email;
        Password = password;
    }
}
