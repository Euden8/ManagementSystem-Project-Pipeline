using MediatR;

namespace ManagementSystem.Application.Projects.Commands.MoveProjectToPhase;

public record MoveProjectToPhaseCommand(
    Guid ProjectId,
    Guid TargetPhaseId,
    string? Note
) : IRequest<Guid>;