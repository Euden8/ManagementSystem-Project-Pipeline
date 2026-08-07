using MediatR;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Application.Common.Interfaces;

namespace ManagementSystem.Application.Timeline;

public class GetProjectTimelineQueryHandler : IRequestHandler<GetProjectTimelineQuery, ProjectTimelineResponseDto?>
{
    private readonly IApplicationDbContext _context;

    public GetProjectTimelineQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectTimelineResponseDto?> Handle(GetProjectTimelineQuery request, CancellationToken cancellationToken)
    {
        var projectExists = await _context.Projects
            .AnyAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (!projectExists)
            return null;

        var timelineEvents = await _context.ProjectPhaseHistories
            .Where(h => h.ProjectId == request.ProjectId)
            .OrderBy(h => h.ChangedAt)
            .Select(h => new ProjectTimelineDto
            {
                HistoryId = h.Id,
                FromPhaseId = h.FromPhaseId,
                ToPhaseId = h.ToPhaseId,
                ChangedByUserId = h.ChangedByUserId,
                ChangedAt = h.ChangedAt,
                DurationInPreviousPhase = h.DurationInPreviousPhase,
                Note = h.Note
            })
            .ToListAsync(cancellationToken);

        var attachments = await _context.Attachments
            .Where(a => a.ProjectId == request.ProjectId && !a.IsDeleted)
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new AttachmentDto
            {
                Id = a.Id,
                Kind = a.Kind.ToString(),
                FileName = a.FileName,
                ContentType = a.ContentType,
                SizeBytes = a.SizeBytes,
                ExternalUrl = a.ExternalUrl,
                Caption = a.Caption,
                UploadedByUserId = a.UploadedByUserId,
                UploadedAt = a.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return new ProjectTimelineResponseDto
        {
            ProjectId = request.ProjectId,
            Timeline = timelineEvents,
            Attachments = attachments
        };
    }
}