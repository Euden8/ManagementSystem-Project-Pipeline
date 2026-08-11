using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain.Entities;
using MediatR;

namespace ManagementSystem.Application.Attachments.Commands.CreateAttachment;

public class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateAttachmentCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            throw new UnauthorizedAccessException(
                "An authenticated user is required to create an attachment.");
        }

        var attachment = new Attachment(currentUserId)
        {
            Id = Guid.NewGuid(),
            ProjectId = request.ProjectId,
            FileName = request.FileName,
            ContentType = "application/octet-stream",
            StorageKey = request.FilePath,
            SizeBytes = request.FileSizeBytes,
            Kind = AttachmentKind.Document,
            UploadedByUserId = currentUserId
        };

        _context.Attachments.Add(attachment);
        await _context.SaveChangesAsync(cancellationToken);

        return attachment.Id;
    }
}
