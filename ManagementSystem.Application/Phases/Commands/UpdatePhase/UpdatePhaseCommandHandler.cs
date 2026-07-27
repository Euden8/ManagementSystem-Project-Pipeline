using MediatR;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Infrastructure;

namespace ManagementSystem.Application.Phases.Commands.UpdatePhase;

public class UpdatePhaseCommandHandler : IRequestHandler<UpdatePhaseCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdatePhaseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdatePhaseCommand request, CancellationToken cancellationToken)
    {
        var phase = await _context.Phases.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (phase is null) return false;

        phase.Name = request.Name;
        phase.Sequence = request.Sequence;
        phase.ColorHex = request.ColorHex;
        phase.IsInitial = request.IsInitial;
        phase.IsTerminal = request.IsTerminal;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}