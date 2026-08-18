using MediatR;

namespace ManagementSystem.Application.Phases.Commands.UpdatePhase;

public record UpdatePhaseCommand(
    Guid Id,
    string Name,
    int Sequence,
    string ColorHex,
    bool IsInitial,
    bool IsTerminal
) : IRequest<bool>;