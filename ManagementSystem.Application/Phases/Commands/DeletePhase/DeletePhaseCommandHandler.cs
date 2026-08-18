using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Phases.Commands.DeletePhase;

public class DeletePhaseCommandHandler : IRequestHandler<DeletePhaseCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeletePhaseCommandHandler(
        IApplicationDbContext context, 
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(DeletePhaseCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(currentUserId))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var isInUse = await _context.Projects
            .AnyAsync(p => p.CurrentPhaseId == request.Id, cancellationToken);

        //if (isInUse)
        //{
            //throw new ValidationException("Cannot delete a phase that is assigned to a project.");
        //}

        var phase = await _context.Phases
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (phase is null)
        {
            return false;
        }

        var oldValuesJson = JsonSerializer.Serialize(new
        {
            phase.Name,
            phase.Sequence,
            phase.ColorHex,
            phase.IsInitial,
            phase.IsTerminal,
            phase.IsActive
        });

        var auditLog = new PhaseAuditLog
        {
            Id = Guid.NewGuid(),
            PhaseId = phase.Id,
            Action = "Deleted",
            ChangedByUserId = currentUserId,
            ChangedAt = DateTime.UtcNow,
            OldValues = oldValuesJson,
            NewValues = null
        };

        _context.PhaseAuditLogs.Add(auditLog);

        _context.Phases.Remove(phase);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}