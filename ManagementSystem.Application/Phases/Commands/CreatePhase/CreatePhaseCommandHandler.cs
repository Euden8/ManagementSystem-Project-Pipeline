using ManagementSystem.Domain;
using ManagementSystem.Infrastructure.Persistence.Repositories;
using MediatR;

namespace ManagementSystem.Application.Phases.Commands.CreatePhase;

public class CreatePhaseCommandHandler : IRequestHandler<CreatePhaseCommand, Guid>
{
    private readonly IPhaseRepository _phaseRepository;

    public CreatePhaseCommandHandler(IPhaseRepository phaseRepository)
    {
        _phaseRepository = phaseRepository;
    }

    public async Task<Guid> Handle(CreatePhaseCommand request, CancellationToken cancellationToken)
    {
        var phase = Phase.Create(
            request.Name,
            request.Sequence,
            request.ColorHex,
            request.IsInitial,
            request.IsTerminal);

        await _phaseRepository.AddAsync(phase, cancellationToken);
        await _phaseRepository.SaveChangesAsync(cancellationToken);

        return phase.Id;
    }
}