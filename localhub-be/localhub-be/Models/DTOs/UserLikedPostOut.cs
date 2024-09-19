namespace localhub_be.Models.DTOs;
public sealed record UserLikedPostOut {
    public Boolean? IsLiked { get; init; }

    public UserLikedPostOut(bool? isLiked) {
        IsLiked = isLiked;
    }
}
