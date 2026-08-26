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
        var phase = new Phase
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Sequence = request.Sequence,
            ColorHex = request.ColorHex,
            IsInitial = request.IsInitial,
            IsTerminal = request.IsTerminal,
            IsActive = true
        };

        _context.Phases.Add(phase);

        _context.PhaseAuditLogs.Add(new PhaseAuditLog
        {
            Id = Guid.NewGuid(),
            PhaseId = phase.Id,
            Action = "Created",
            ChangedByUserId = _currentUserService.UserId ?? "System",
            ChangedAt = DateTime.UtcNow,
            OldValues = null,
            NewValues = $"Name: {phase.Name}, Sequence: {phase.Sequence}"
        });

        await _context.SaveChangesAsync(cancellationToken);

        return phase.Id;
    }
}