using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Attachments.Queries.GetAttachmentById;

public record GetAttachmentByIdQuery(Guid Id) : IRequest<Attachment?>;

public class GetAttachmentByIdQueryHandler : IRequestHandler<GetAttachmentByIdQuery, Attachment?>
{
    private readonly IApplicationDbContext _context;

    public GetAttachmentByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Attachment?> Handle(GetAttachmentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Attachments
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
    }
}