using System.ComponentModel.DataAnnotations;

namespace localhub_be.Models.DTOs;
public sealed record CommentIn {
    [Required]
    public Guid UserId { get; init; }
    [Required]
    public string? Content { get; init; }

    public CommentIn(Guid userId, string? content) {
        UserId = userId;
        Content = content;
    }
}
