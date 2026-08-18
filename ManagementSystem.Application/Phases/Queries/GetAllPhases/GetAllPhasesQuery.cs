using MediatR;

namespace ManagementSystem.Application.Phases.Queries.GetAllPhases;

public record GetAllPhasesQuery : IRequest<List<PhaseDto>>;