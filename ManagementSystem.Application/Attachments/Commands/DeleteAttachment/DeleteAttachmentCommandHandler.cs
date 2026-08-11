using ManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace ManagementSystem.Application.Attachments.Commands.DeleteAttachment;

public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand, bool>
{
    private readonly IApplicationDbContext _context;


    public DeleteAttachmentCommandHandler(IApplicationDbContext context)
    {
        _context = context; 
    }

    public async Task<bool> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        // kerkon recordin e bashkuar me ID dhe nese nuk ekziston kthen false 
        var attachment = await _context.Attachments.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (attachment == null) return false;

        _context.Attachments.Remove(attachment);
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}