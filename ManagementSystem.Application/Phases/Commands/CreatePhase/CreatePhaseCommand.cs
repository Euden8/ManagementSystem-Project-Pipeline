using MediatR;

namespace ManagementSystem.Application.Phases.Commands.CreatePhase;

public record CreatePhaseCommand(
    string Name,
    int Sequence,
    string ColorHex,
    bool IsInitial,
    bool IsTerminal
) : IRequest<Guid>;