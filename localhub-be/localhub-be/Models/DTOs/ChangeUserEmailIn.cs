using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record ChangeUserEmailIn {
    [Required]
    [EmailAddress]
    public string? Email { get; init; }

    public ChangeUserEmailIn(string? email) {
        Email = email;
    }
}
