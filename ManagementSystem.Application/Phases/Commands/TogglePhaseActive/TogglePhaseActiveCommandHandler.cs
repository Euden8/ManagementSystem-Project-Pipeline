using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Phases.Commands.TogglePhaseActive;

public class TogglePhaseActiveCommandHandler : IRequestHandler<TogglePhaseActiveCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public TogglePhaseActiveCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(TogglePhaseActiveCommand request, CancellationToken cancellationToken)
    {
        var phase = await _context.Phases
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        
        if (phase is null)
            return false;   
        

        var oldValues = $"IsActive: {phase.IsActive}";
        
        phase.IsActive = !phase.IsActive;

        _context.PhaseAuditLogs.Add(new PhaseAuditLog
        {
            Id = Guid.NewGuid(),
            PhaseId = phase.Id,
            Action = phase.IsActive ? "Activated" : "Deactivated",
            ChangedByUserId = _currentUserService.UserId ?? "System",
            ChangedAt = DateTime.UtcNow,
            OldValues = oldValues, // <-- Fixed here
            NewValues = $"IsActive: {phase.IsActive}"
        });

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}