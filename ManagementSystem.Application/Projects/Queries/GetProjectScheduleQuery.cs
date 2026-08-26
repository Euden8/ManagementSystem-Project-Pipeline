using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Projects.Queries.GetProjectScheduleVariance;

public record GetProjectScheduleVarianceQuery(bool AtRiskOnly = true) : IRequest<List<ProjectScheduleVarianceDto>>;

public class GetProjectScheduleVarianceQueryHandler : IRequestHandler<GetProjectScheduleVarianceQuery, List<ProjectScheduleVarianceDto>>
{
    private readonly ApplicationDbContext _context;

    public GetProjectScheduleVarianceQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectScheduleVarianceDto>> Handle(GetProjectScheduleVarianceQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var projects = await _context.Projects
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = projects
    .Select(p =>
    {
        var now = DateTime.UtcNow;

        DateTime plannedStart = p.PlannedStartDate ?? now;
        DateTime plannedEnd = p.PlannedEndDate ?? now;
        DateTime actualStart = p.ActualStartDate ?? now;
        DateTime actualEnd = p.ActualEndDate ?? now;

        int startVariance = p.ActualStartDate.HasValue
            ? (int)(p.ActualStartDate.Value - plannedStart).TotalDays
            : 0;

        int endVariance = (int)(actualEnd - plannedEnd).TotalDays;

        bool isAtRisk = endVariance > 0 || (!p.ActualStartDate.HasValue && now > plannedStart);

        return new ProjectScheduleVarianceDto
        {
            ProjectId = p.Id,
            ProjectName = p.Title,
            PlannedStartDate = plannedStart,
            PlannedEndDate = plannedEnd,
            ActualStartDate = p.ActualStartDate,
            ActualEndDate = p.ActualEndDate,
            StartVarianceDays = startVariance,
            EndVarianceDays = endVariance,
            IsAtRisk = isAtRisk,
            RiskReason = isAtRisk ? $"Project schedule delayed by {endVariance} day(s)." : "On schedule"
        };
    })
    .Where(p => !request.AtRiskOnly || p.IsAtRisk)
    .ToList();

        return result;
    }
}