using MediatR;

namespace ManagementSystem.Application.Timeline;

public record GetProjectTimelineQuery(Guid ProjectId) : IRequest<ProjectTimelineResponseDto?>;