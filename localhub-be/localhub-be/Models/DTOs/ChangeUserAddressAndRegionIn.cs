using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record ChangeUserAddressAndRegionIn {
    [Required]
    [RegularExpression(@"^([A-Z][a-z]+(\s[A-Z][a-z]+)*\s)?\d+$", ErrorMessage = "Invalid address. Valid: Address bb.")]
    [StringLength(64, MinimumLength = 6, ErrorMessage = "Invalid address name. Address name should be at least 6 characters long.")]
    public string? Address { get; init; }

    [Required]
    [RegularExpression(@"^([A-Z][a-z]*)(\s[A-Z][a-z]*)*$", ErrorMessage = "Invalid region name. Valid: Region Name.")]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "Invalid region name. Region name should be at least 3 characters long.")]
    public string? Region { get; init; }

    public ChangeUserAddressAndRegionIn(string? address, string? region) {
        Address = address;
        Region = region;
    }
}
