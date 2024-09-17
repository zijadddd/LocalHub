namespace localhub_be.Models.DTOs;
public sealed record LikeAndCommentCountOut {
    public int NumberOfLikes { get; init; }
    public int NumberOfComments { get; init; }

    public LikeAndCommentCountOut(int numberOfLikes, int numberOfComments) {
        NumberOfLikes = numberOfLikes;
        NumberOfComments = numberOfComments;
    }
}

