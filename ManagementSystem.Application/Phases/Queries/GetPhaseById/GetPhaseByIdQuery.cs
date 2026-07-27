using MediatR;

namespace ManagementSystem.Application.Phases.Queries.GetPhaseById;

public record GetPhaseByIdQuery(Guid Id) : IRequest<PhaseDto?>;