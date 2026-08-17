using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
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

        var currentUserId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(currentUserId))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }


        var phase = await _context.Phases
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (phase is null)
        {
            throw new NotFoundException(nameof(Phase), request.Id);
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


        phase.Name = request.Name;
        phase.Sequence = request.Sequence;
        phase.ColorHex = request.ColorHex;
        phase.IsInitial = request.IsInitial;
        phase.IsTerminal = request.IsTerminal;
        phase.IsActive = request.IsActive;


        var newValuesJson = JsonSerializer.Serialize(new
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
            Action = "Updated",
            ChangedByUserId = currentUserId,
            ChangedAt = DateTime.UtcNow,
            OldValues = oldValuesJson,
            NewValues = newValuesJson
        };

        _context.PhaseAuditLogs.Add(auditLog);


        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}