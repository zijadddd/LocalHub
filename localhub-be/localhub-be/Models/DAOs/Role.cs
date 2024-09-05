namespace localhub_be.Models.DAOs;
public sealed class Role {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Auth? Auth { get; set; }
}
