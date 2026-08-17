using System.Text.Json;
using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain;
using ManagementSystem.Domain.Entities;
using MediatR;

namespace ManagementSystem.Application.Phases.Commands.CreatePhase;

public class CreatePhaseCommandHandler : IRequestHandler<CreatePhaseCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreatePhaseCommandHandler(
        IApplicationDbContext context, 
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreatePhaseCommand request, CancellationToken cancellationToken)
    {
        
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

      
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

        var auditLog = new PhaseAuditLog
        {
            Id = Guid.NewGuid(),
            PhaseId = phase.Id,
            Action = "Created",
            ChangedByUserId = currentUserId,
            ChangedAt = DateTime.UtcNow,
            OldValues = null,
            NewValues = JsonSerializer.Serialize(new
            {
                phase.Name,
                phase.Sequence,
                phase.ColorHex,
                phase.IsInitial,
                phase.IsTerminal,
                phase.IsActive
            })
        };

        _context.PhaseAuditLogs.Add(auditLog);

    
        await _context.SaveChangesAsync(cancellationToken);

        return phase.Id;
    }
}