using MediatR;

namespace ManagementSystem.Application.Phases.Commands.TogglePhaseActive;

public record TogglePhaseActiveCommand(Guid Id) : IRequest<bool>;