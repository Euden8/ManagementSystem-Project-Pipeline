using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Phases.Commands.UpdatePhase;

public class UpdatePhaseCommandHandler : IRequestHandler<UpdatePhaseCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePhaseCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(UpdatePhaseCommand request, CancellationToken cancellationToken)
    {
        var phase = await _context.Phases
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (phase is null) 
            return false;

        var oldValues = $"Name: {phase.Name}, Sequence: {phase.Sequence}, ColorHex: {phase.ColorHex}, IsInitial: {phase.IsInitial}, IsTerminal: {phase.IsTerminal}";

        phase.Name = request.Name;
        phase.Sequence = request.Sequence;
        phase.ColorHex = request.ColorHex;
        phase.IsInitial = request.IsInitial;
        phase.IsTerminal = request.IsTerminal;

        var newValues = $"Name: {phase.Name}, Sequence: {phase.Sequence}, ColorHex: {phase.ColorHex}, IsInitial: {phase.IsInitial}, IsTerminal: {phase.IsTerminal}";

        _context.PhaseAuditLogs.Add(new PhaseAuditLog
        {
            Id = Guid.NewGuid(),
            PhaseId = phase.Id,
            Action = "Updated",
            ChangedByUserId = _currentUserService.UserId ?? "System",
            ChangedAt = DateTime.UtcNow,
            OldValues = oldValues,
            NewValues = newValues
        });

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
