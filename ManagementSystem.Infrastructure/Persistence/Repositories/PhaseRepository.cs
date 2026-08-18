using ManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Infrastructure.Persistence.Repositories;

public sealed class PhaseRepository : IPhaseRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PhaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Phase?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return _dbContext.Phases
            .FirstOrDefaultAsync(phase => phase.Id == id, cancellationToken);
    }
}

public interface IPhaseRepository
{
    Task<Phase?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}