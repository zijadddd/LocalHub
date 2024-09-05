namespace localhub_be.Models.DTOs;
public sealed record AuthOut {
    public string? Token { get; init; }

    public AuthOut(string token) {
        Token = token;
    }
}
