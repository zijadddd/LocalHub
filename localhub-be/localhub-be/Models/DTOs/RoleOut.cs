namespace localhub_be.Models.DTOs;
public sealed record RoleOut {
    public string? Name { get; init; }

    public RoleOut(string? name) {
        Name = name;
    }
}
