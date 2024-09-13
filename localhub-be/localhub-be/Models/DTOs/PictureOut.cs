namespace localhub_be.Models.DTOs;
public sealed record PictureOut {
    public string? FilePath { get; init; }

    public PictureOut(string? filePath) {
        FilePath = filePath;
    }
}
