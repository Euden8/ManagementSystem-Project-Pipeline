namespace ManagementSystem.Application.Timeline;

public class ProjectTimelineDto
{
    public Guid HistoryId { get; set; }
    public Guid? FromPhaseId { get; set; }
    public Guid ToPhaseId { get; set; }
    public string ChangedByUserId { get; set; } = string.Empty;
    public DateTime ChangedAt { get; set; }
    public TimeSpan? DurationInPreviousPhase { get; set; }
    public string? Note { get; set; }
}

public class ProjectTimelineResponseDto
{
    public Guid ProjectId { get; set; }
    public List<ProjectTimelineDto> Timeline { get; set; } = new();
    public List<AttachmentDto> Attachments { get; set; } = new();
}