using MediatR;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Infrastructure;

namespace ManagementSystem.Application.Phases.Queries.GetPhaseById;

public class GetPhaseByIdQueryHandler : IRequestHandler<GetPhaseByIdQuery, PhaseDto?>
{
    private readonly ApplicationDbContext _context;

    public GetPhaseByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PhaseDto?> Handle(GetPhaseByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Phases
            .Where(p => p.Id == request.Id)
            .Select(p => new PhaseDto(p.Id, p.Name, p.Sequence, p.ColorHex, p.IsInitial, p.IsTerminal, p.IsActive))
            .FirstOrDefaultAsync(cancellationToken);
    }
}