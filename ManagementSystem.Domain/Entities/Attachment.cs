namespace ManagementSystem.Domain.Entities;

public enum AttachmentKind
{
    Image = 1,
    Photo = 2,
    Document = 3,
    Link = 4
}

public class Attachment
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public AttachmentKind Kind { get; private set; }
    public string FileName { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long SizeBytes { get; private set; }
    public string StorageKey { get; private set; } = string.Empty;
    public string? ExternalUrl { get; private set; }
    public string? Caption { get; private set; }
    public string UploadedByUserId { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }
    public string CreatedBy { get; private set; } = string.Empty;
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedBy { get; private set; }

    private Attachment() { } 

    public Attachment(
        Guid id,
        Guid projectId,
        AttachmentKind kind,
        string fileName,
        string contentType,
        long sizeBytes,
        string storageKey,
        string uploadedByUserId,
        string? externalUrl = null,
        string? caption = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id is required.", nameof(id));
        if (projectId == Guid.Empty) throw new ArgumentException("ProjectId is required.", nameof(projectId));
        if (string.IsNullOrWhiteSpace(uploadedByUserId)) throw new ArgumentException("UploadedByUserId is required.", nameof(uploadedByUserId));

        Id = id;
        ProjectId = projectId;
        Kind = kind;
        FileName = fileName;
        ContentType = contentType;
        SizeBytes = sizeBytes;
        StorageKey = storageKey;
        ExternalUrl = externalUrl;
        Caption = caption;
        UploadedByUserId = uploadedByUserId;
        CreatedAt = DateTime.UtcNow;
        CreatedBy = uploadedByUserId;
        IsDeleted = false;
    }

    public void SoftDelete(string deletedByUserId)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = deletedByUserId;
    }
}