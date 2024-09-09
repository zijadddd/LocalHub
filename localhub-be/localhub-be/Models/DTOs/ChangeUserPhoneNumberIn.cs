using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record ChangeUserPhoneNumberIn {
    [Required]
    [Phone]
    public string? PhoneNumber { get; init; }

    public ChangeUserPhoneNumberIn(string? phoneNumber) {
        PhoneNumber = phoneNumber;
    }
}
