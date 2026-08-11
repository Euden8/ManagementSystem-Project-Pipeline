using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Attachments.Queries.GetAttachmentsByProject;

public class GetAttachmentsByProjectQueryHandler : IRequestHandler<GetAttachmentsByProjectQuery, List<Attachment>>
{
    private readonly IApplicationDbContext _context;

    public GetAttachmentsByProjectQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Attachment>> Handle(GetAttachmentsByProjectQuery request, CancellationToken cancellationToken)
    {
        return await _context.Attachments
            .Where(a => a.ProjectId == request.ProjectId)
            .ToListAsync(cancellationToken);
    }
}
