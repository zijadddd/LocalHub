namespace localhub_be.Models.DTOs;
public sealed record MessageOut {
    public string Message { get; init; }

    public MessageOut(string message) {
        Message = message;
    }
}
