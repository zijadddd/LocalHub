using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record PostIn {
    [Required]
    public string? Name { get; init; }
    [Required]
    public string? Description { get; init; }
    [Display(Name = "Picture")]
    public IFormFile? Image { get; init; }
}
