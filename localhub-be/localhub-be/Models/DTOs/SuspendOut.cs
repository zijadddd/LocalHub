namespace localhub_be.Models.DTOs;
public sealed record SuspendOut {
    public string? Reason { get; init; }
    public bool IsSuspended { get; init; }

    public SuspendOut(string? reason, bool isSuspended) {
        Reason = reason;
        IsSuspended = isSuspended;
    }
}

