namespace ManagementSystem.Domain.Entities;

public class Attachment : BaseEntity
{
    public Attachment(string createdBy) : base(createdBy)
    {
    }


    protected Attachment() : base("System")
    {
    }

    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public AttachmentKind Kind { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public string? StorageKey { get; set; }
    public string? ExternalUrl { get; set; }
    public string? Caption { get; set; }
    public string UploadedByUserId { get; set; } = string.Empty;


    public PipelineProject Project { get; set; } = null!;
    public ApplicationUser UploadedByUser { get; set; } = null!;
}

public enum AttachmentKind
{
    Image,
    Photo,
    Document,
    Link
}