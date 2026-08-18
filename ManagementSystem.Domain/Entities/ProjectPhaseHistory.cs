namespace ManagementSystem.Domain.Entities;

public class ProjectPhaseHistory : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public Guid? FromPhaseId { get; private set; }
    public Guid ToPhaseId { get; private set; }
    public string ChangedByUserId { get; private set; } = string.Empty;
    public DateTime ChangedAt { get; private set; }
    public TimeSpan? DurationInPreviousPhase { get; private set; }
    public string? Note { get; private set; }

    public PipelineProject Project { get; private set; } = null!;
    public Phase? FromPhase { get; private set; }
    public Phase ToPhase { get; private set; } = null!;
    public ApplicationUser ChangedByUser { get; private set; } = null!;

    private ProjectPhaseHistory() : base("System")
    {
    }

    private ProjectPhaseHistory(
        Guid projectId,
        Guid? fromPhaseId,
        Guid toPhaseId,
        string changedByUserId,
        DateTime changedAt,
        TimeSpan? durationInPreviousPhase,
        string? note)
        : base(changedByUserId)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        FromPhaseId = fromPhaseId;
        ToPhaseId = toPhaseId;
        ChangedByUserId = changedByUserId;
        ChangedAt = changedAt;
        DurationInPreviousPhase = durationInPreviousPhase;
        Note = note?.Trim();
    }

    public static ProjectPhaseHistory Create(
        Guid projectId,
        Phase? fromPhase,
        Phase toPhase,
        string changedByUserId,
        DateTime changedAt,
        DateTime? enteredFromPhaseAt,
        string? note = null)
    {
        if (toPhase is null)
            throw new ArgumentNullException(nameof(toPhase));

        if (string.IsNullOrWhiteSpace(changedByUserId))
            throw new ArgumentException("ChangedByUserId cannot be empty.", nameof(changedByUserId));

        TimeSpan? duration = enteredFromPhaseAt.HasValue
            ? changedAt - enteredFromPhaseAt.Value
            : null;

        return new ProjectPhaseHistory(
            projectId,
            fromPhase?.Id,
            toPhase.Id,
            changedByUserId,
            changedAt,
            duration,
            note);
    }
}