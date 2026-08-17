namespace ManagementSystem.Domain.Entities;

public class PhaseAuditLog
{
    public Guid Id { get; set; }
    public Guid PhaseId { get; set; }
    public string Action { get; set; } = string.Empty; 
    public string ChangedByUserId { get; set; } = string.Empty;
    public DateTime ChangedAt { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }

    public Phase Phase { get; set; } = null!;
}