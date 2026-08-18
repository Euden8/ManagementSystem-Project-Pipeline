using MediatR;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Infrastructure;

namespace ManagementSystem.Application.Phases.Queries.GetAllPhases;

public class GetAllPhasesQueryHandler : IRequestHandler<GetAllPhasesQuery, List<PhaseDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllPhasesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PhaseDto>> Handle(GetAllPhasesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Phases
            .OrderBy(p => p.Sequence)
            .Select(p => new PhaseDto(p.Id, p.Name, p.Sequence, p.ColorHex, p.IsInitial, p.IsTerminal, p.IsActive))
            .ToListAsync(cancellationToken);
    }
}