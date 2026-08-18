using MediatR;
using ManagementSystem.Application.Common.Interfaces;

namespace ManagementSystem.Application.Projects.Queries.GetProjectSlaMetrics;

public class GetProjectSlaMetricsQueryHandler : IRequestHandler<GetProjectSlaMetricsQuery, ProjectSlaMetricsDto>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectSlaMetricsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public Task<ProjectSlaMetricsDto> Handle(GetProjectSlaMetricsQuery request, CancellationToken cancellationToken)
    {
        // Placeholder logic until ProjectPhaseHistory entity and repository queries are merged
        var phaseDurations = new List<PhaseDurationDto>();

        double totalCycleTimeDays = phaseDurations.Sum(p => p.DurationDays);
        bool isSlaBreached = phaseDurations.Any(p => p.IsExceeded);

        var response = new ProjectSlaMetricsDto
        {
            ProjectId = request.ProjectId,
            TotalCycleTimeDays = Math.Round(totalCycleTimeDays, 2),
            IsSlaBreached = isSlaBreached,
            PhaseDurations = phaseDurations
        };

        return Task.FromResult(response);
    }
}