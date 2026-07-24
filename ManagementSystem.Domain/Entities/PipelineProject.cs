namespace ManagementSystem.Domain.Entities;

public class PipelineProject : BaseEntity
{
    public Guid Id { get; private set; }

    public string Code { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public Guid CurrentPhaseId { get; private set; }

    public Phase? CurrentPhase { get; private set; }

    public ProjectPriority Priority { get; private set; }

    public string OwnerUserId { get; private set; }

    public DateTime? PlannedStartDate { get; private set; }

    public DateTime? PlannedEndDate { get; private set; }

    public DateTime? ActualStartDate { get; private set; }

    public DateTime? ActualEndDate { get; private set; }

    public PipelineProject(
        Guid id,
        string code,
        string title,
        Guid currentPhaseId,
        ProjectPriority priority,
        string ownerUserId,
        string createdBy,
        string? description = null)
        : base(createdBy)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Project ID cannot be empty.", nameof(id));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Project code cannot be empty.", nameof(code));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Project title cannot be empty.", nameof(title));

        if (currentPhaseId == Guid.Empty)
            throw new ArgumentException("Current phase ID cannot be empty.", nameof(currentPhaseId));

        if (string.IsNullOrWhiteSpace(ownerUserId))
            throw new ArgumentException("Owner user ID cannot be empty.", nameof(ownerUserId));

        if (!Enum.IsDefined(priority))
            throw new ArgumentOutOfRangeException(nameof(priority));

        Id = id;
        Code = code.Trim();
        Title = title.Trim();
        Description = description?.Trim();
        CurrentPhaseId = currentPhaseId;
        Priority = priority;
        OwnerUserId = ownerUserId;
    }

    public void UpdateDetails(
        string code,
        string title,
        string? description,
        ProjectPriority priority,
        string modifiedBy)
    {
        EnsureNotDeleted();

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Project code cannot be empty.", nameof(code));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Project title cannot be empty.", nameof(title));

        if (!Enum.IsDefined(priority))
            throw new ArgumentOutOfRangeException(nameof(priority));

        Code = code.Trim();
        Title = title.Trim();
        Description = description?.Trim();
        Priority = priority;

        MarkModified(modifiedBy);
    }

    public void ChangePhase(Guid phaseId, string modifiedBy)
    {
        EnsureNotDeleted();

        if (phaseId == Guid.Empty)
            throw new ArgumentException("Phase ID cannot be empty.", nameof(phaseId));

        CurrentPhaseId = phaseId;
        MarkModified(modifiedBy);
    }

    public void ChangeOwner(string ownerUserId, string modifiedBy)
    {
        EnsureNotDeleted();

        if (string.IsNullOrWhiteSpace(ownerUserId))
            throw new ArgumentException("Owner user ID cannot be empty.", nameof(ownerUserId));

        OwnerUserId = ownerUserId;
        MarkModified(modifiedBy);
    }

    public void SetPlannedDates(
        DateTime? plannedStartDate,
        DateTime? plannedEndDate,
        string modifiedBy)
    {
        EnsureNotDeleted();

        if (plannedStartDate.HasValue &&
            plannedEndDate.HasValue &&
            plannedEndDate < plannedStartDate)
        {
            throw new ArgumentException(
                "Planned end date cannot be earlier than planned start date.");
        }

        PlannedStartDate = plannedStartDate;
        PlannedEndDate = plannedEndDate;

        MarkModified(modifiedBy);
    }

    public void Start(DateTime actualStartDate, string modifiedBy)
    {
        EnsureNotDeleted();

        ActualStartDate = actualStartDate;
        MarkModified(modifiedBy);
    }

    public void Complete(DateTime actualEndDate, string modifiedBy)
    {
        EnsureNotDeleted();

        if (!ActualStartDate.HasValue)
        {
            throw new InvalidOperationException(
                "A project must be started before it can be completed.");
        }

        if (actualEndDate < ActualStartDate.Value)
        {
            throw new ArgumentException(
                "Actual end date cannot be earlier than actual start date.",
                nameof(actualEndDate));
        }

        ActualEndDate = actualEndDate;
        MarkModified(modifiedBy);
    }

    public void SoftDelete(string deletedBy)
    {
        MarkDeleted(deletedBy);
    }

    private void EnsureNotDeleted()
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException(
                "A deleted project cannot be changed.");
        }
    }
}