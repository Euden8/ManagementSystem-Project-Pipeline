using MediatR;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Infrastructure;

namespace ManagementSystem.Application.Phases.Commands.DeletePhase;

public class DeletePhaseCommandHandler : IRequestHandler<DeletePhaseCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeletePhaseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeletePhaseCommand request, CancellationToken cancellationToken)
    {
        var phase = await _context.Phases.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (phase is null) return false;

        phase.IsActive = false; 
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}