using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record CommentIn {
    [Required]
    public string? Content { get; init; }

    public CommentIn(string? content) {
        Content = content;
    }
}
