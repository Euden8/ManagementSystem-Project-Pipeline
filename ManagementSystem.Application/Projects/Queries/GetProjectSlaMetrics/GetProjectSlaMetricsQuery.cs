using MediatR;

namespace ManagementSystem.Application.Projects.Queries.GetProjectSlaMetrics;

public record GetProjectSlaMetricsQuery(
    Guid ProjectId,
    double DefaultSlaThresholdDays
) : IRequest<ProjectSlaMetricsDto>;