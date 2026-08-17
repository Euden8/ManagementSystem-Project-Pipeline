using MediatR;

namespace ManagementSystem.Application.Attachments.Commands.CreateAttachment;

public record CreateAttachmentCommand(
    Guid ProjectId,
    string FileName,
    string FilePath,
    long FileSizeBytes
) : IRequest<Guid>;