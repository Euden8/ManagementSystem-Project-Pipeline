namespace ManagementSystem.Domain.Entities;

public class ProjectPhaseHistory : BaseEntity
{
    public ProjectPhaseHistory(string createdBy) : base(createdBy)
    {
    }

    
    protected ProjectPhaseHistory() : base("System")
    {
    }

    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid? FromPhaseId { get; set; }
    public Guid ToPhaseId { get; set; }
    public string ChangedByUserId { get; set; } = string.Empty;
    public DateTime ChangedAt { get; set; }
    public TimeSpan? DurationInPreviousPhase { get; set; }
    public string? Note { get; set; }


    public PipelineProject Project { get; set; } = null!;
    public Phase? FromPhase { get; set; }  
    public Phase ToPhase { get; set; } = null!;
    public ApplicationUser ChangedByUser { get; set; } = null!;
}