namespace localhub_be.Models.DAOs;
public sealed class SuspendInfo {
    public Guid Id { get; set; }
    public string? Reason { get; set; }
    public DateTime? Created { get; init; } = DateTime.Now;
    public Guid AuthId { get; set; }
    public Auth? Auth { get; set; }
}

