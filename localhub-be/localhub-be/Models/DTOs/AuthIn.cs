using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record AuthIn {
    [Required]
    [EmailAddress]
    public string? Email { get; init; }

    [Required]
    [StringLength(64, MinimumLength = 8, ErrorMessage = "Invalid password. The password should be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Invalid password. Valid: Abcdef1*")]
    public string? Password { get; init; }

    public AuthIn(string email, string password) {
        Email = email;
        Password = password;
    }
}
