using ManagementSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Attachments.Commands.UpdateAttachment;

public record UpdateAttachmentCommand(
    Guid Id,
    string FileName,
    string? Caption
) : IRequest<bool>;

public class UpdateAttachmentCommandHandler : IRequestHandler<UpdateAttachmentCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateAttachmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = await _context.Attachments.FindAsync(new object[] { request.Id }, cancellationToken);

        if (attachment == null)
        {
            return false;
        }

        attachment.FileName = request.FileName;
        attachment.Caption = request.Caption;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}