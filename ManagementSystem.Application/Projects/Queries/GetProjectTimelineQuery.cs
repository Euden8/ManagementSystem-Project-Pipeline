using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Projects.Queries.GetProjectTimeline;

public record GetProjectTimelineQuery(Guid ProjectId) : IRequest<List<ProjectTimelineEventDto>>;

public class GetProjectTimelineQueryHandler : IRequestHandler<GetProjectTimelineQuery, List<ProjectTimelineEventDto>>
{
    private readonly ApplicationDbContext _context;

    public GetProjectTimelineQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectTimelineEventDto>> Handle(GetProjectTimelineQuery request, CancellationToken cancellationToken)
    {
        // 1. Fetch Phase Transition History Events
        var phaseEvents = await _context.Set<Domain.Entities.ProjectPhaseHistory>()
            .AsNoTracking()
            .Where(h => h.ProjectId == request.ProjectId)
            .Include(h => h.FromPhase)
            .Include(h => h.ToPhase)
            .Select(h => new ProjectTimelineEventDto
            {
                EventId = h.Id,
                EventType = "PhaseChange",
                EventDate = h.CreatedAt,
                Description = h.Note ?? $"Phase transitioned from {h.FromPhase.Name} to {h.ToPhase.Name}",
                PerformedBy = h.CreatedBy,
                FromPhaseName = h.FromPhase != null ? h.FromPhase.Name : null,
                ToPhaseName = h.ToPhase != null ? h.ToPhase.Name : null
            })
            .ToListAsync(cancellationToken);

        var attachmentEvents = await _context.Set<Domain.Entities.Attachment>()
            .AsNoTracking()
            .Where(a => a.ProjectId == request.ProjectId)
            .Select(a => new ProjectTimelineEventDto
            {
                EventId = a.Id,
                EventType = "AttachmentAdded",
                EventDate = a.CreatedAt,
                Description = string.IsNullOrWhiteSpace(a.Caption)
                    ? $"Attachment '{a.FileName}' was uploaded"
                    : a.Caption,
                PerformedBy = a.CreatedBy,
                FileName = a.FileName,
                AttachmentKind = a.Kind.ToString(),
                ExternalUrl = a.ExternalUrl
            })
            .ToListAsync(cancellationToken);

        var timeline = phaseEvents
            .Concat(attachmentEvents)
            .OrderBy(e => e.EventDate)
            .ToList();

        return timeline;
    }
}