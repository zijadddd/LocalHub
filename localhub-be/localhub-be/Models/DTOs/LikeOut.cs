namespace localhub_be.Models.DTOs;
public sealed record LikeOut {
    public int NumberOfLikes { get; init; }

    public LikeOut(int numberOfLikes) {
        NumberOfLikes = numberOfLikes;
    }
}
