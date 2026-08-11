using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain;
using MediatR;

namespace ManagementSystem.Application.Phases.Commands.CreatePhase;

public class CreatePhaseCommandHandler : IRequestHandler<CreatePhaseCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreatePhaseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreatePhaseCommand request, CancellationToken cancellationToken)
    {
        var phase = new Phase
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Sequence = request.Sequence,
            ColorHex = request.ColorHex,
            IsInitial = request.IsInitial,
            IsTerminal = request.IsTerminal
        };

        _context.Phases.Add(phase);
        await _context.SaveChangesAsync(cancellationToken);

        return phase.Id;
    }
}
