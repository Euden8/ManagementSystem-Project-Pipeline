namespace ManagementSystem.Application.Projects.Queries.GetProjectTimeline;

public class ProjectTimelineEventDto
{
    public Guid EventId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PerformedBy { get; set; } = string.Empty;

    public string? FromPhaseName { get; set; }
    public string? ToPhaseName { get; set; }

    public string? FileName { get; set; }
    public string? AttachmentKind { get; set; }
    public string? ExternalUrl { get; set; }
}