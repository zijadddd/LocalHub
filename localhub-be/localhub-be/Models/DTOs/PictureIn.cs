using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record PictureIn {
    [Required(ErrorMessage = "Please choose an image.")]
    [Display(Name = "Picture")]
    public IFormFile? Image { get; init; }
}
