using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed class ChangeUserPasswordIn {
    [Required]
    [StringLength(64, MinimumLength = 8, ErrorMessage = "Invalid old password. The password should be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Invalid old password. Valid: Abcdef1*")]
    public string? OldPassword { get; init; }

    [Required]
    [StringLength(64, MinimumLength = 8, ErrorMessage = "Invalid new password. The password should be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Invalid new password. Valid: Abcdef1*")]
    public string? NewPassword { get; init; }

    public ChangeUserPasswordIn(string? oldPassword, string? newPassword) {
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}
