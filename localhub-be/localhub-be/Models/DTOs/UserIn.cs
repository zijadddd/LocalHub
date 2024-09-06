using localhub_be.Core.Validators;
using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record UserIn {

    [Required]
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid first name. Valid: John")]
    [StringLength(32, MinimumLength = 2, ErrorMessage = "Invalid first name. First name should be at least 2 characters long.")]
    public string? FirstName { get; init; }

    [Required]
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid last name. Valid: Smith")]
    [StringLength(32, MinimumLength = 2, ErrorMessage = "Invalid last name. Last name should be at least 2 characters long.")]
    public string? LastName { get; init; }

    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid birth date. Valid: 2023-10-17.")]
    public DateOnly BirthDate { get; init; }

    [Required]
    [RegularExpression(@"^([A-Z][a-z]+(\s[A-Z][a-z]+)*\s)?\d+$", ErrorMessage = "Invalid address. Valid: Address bb.")]
    [StringLength(64, MinimumLength = 6, ErrorMessage = "Invalid address name. Address name should be at least 6 characters long.")]
    public string? Address { get; init; }

    [Required]
    [RegularExpression(@"^([A-Z][a-z]*)(\s[A-Z][a-z]*)*$", ErrorMessage = "Invalid region name. Valid: Region Name.")]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "Invalid region name. Region name should be at least 3 characters long.")]
    public string? Region { get; init; }

    [Required]
    [Phone]
    public string? PhoneNumber { get; init; }

    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid membership date. Valid: 2023-10-17.")]
    public DateOnly MembershipDate { get; init; }

    [Required]
    [EmailAddress]
    public string? Email { get; init; }

    [Required]
    [StringLength(64, MinimumLength = 8, ErrorMessage = "Invalid password. The password should be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Invalid password. Valid: Abcdef1*")]
    public string? Password { get; init; }

    [Required]
    [StringLength(12, MinimumLength = 4, ErrorMessage = "Invalid role name. The role name should be between 4 and 12 characters long.")]
    [CustomValidation(typeof(RoleValidator), nameof(RoleValidator.ValidateRole))]
    public string? Role { get; init; }

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
