namespace ManagementSystem.Application.Timeline;

public class AttachmentDto
{
    public Guid Id { get; set; }
    public string Kind { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public string? ExternalUrl { get; set; }
    public string? Caption { get; set; }
    public string UploadedByUserId { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}