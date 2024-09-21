namespace localhub_be.Models.DTOs;
public sealed record LikeOut {
    public int NumberOfLikes { get; init; }
    public bool IsLiked { get; init; }

    public LikeOut(int numberOfLikes, bool isLiked) {
        NumberOfLikes = numberOfLikes;
        IsLiked = isLiked;
    }
}
