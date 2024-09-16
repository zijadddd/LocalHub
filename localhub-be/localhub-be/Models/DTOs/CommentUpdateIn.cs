using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record CommentUpdateIn {
    [Required]
    public string? Content { get; init; }

    public CommentUpdateIn(string? content) {
        Content = content;
    }
}
