using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record SuspendIn {
    [Required]
    [StringLength(255, MinimumLength = 6, ErrorMessage = "Invalid reason. Suspend reason should be at least 6 characters long.")]
    public string Reason { get; init; }

    public SuspendIn(string reason) {
        Reason = reason;
    }
}
