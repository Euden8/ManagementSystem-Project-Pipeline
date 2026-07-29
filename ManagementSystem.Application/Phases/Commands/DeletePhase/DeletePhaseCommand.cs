using MediatR;

namespace ManagementSystem.Application.Phases.Commands.DeletePhase;

public record DeletePhaseCommand(Guid Id) : IRequest<bool>;