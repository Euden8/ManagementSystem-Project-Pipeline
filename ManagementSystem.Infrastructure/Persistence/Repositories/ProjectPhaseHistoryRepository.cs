using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Infrastructure.Persistence.Repositories;

public sealed class ProjectPhaseHistoryRepository : IProjectPhaseHistoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProjectPhaseHistoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ProjectPhaseHistory?> GetLastForProjectAsync(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        return _dbContext.ProjectPhaseHistories
            .Where(history => history.ProjectId == projectId)
            .OrderByDescending(history => history.ChangedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(
        ProjectPhaseHistory historyEntry,
        CancellationToken cancellationToken)
    {
        await _dbContext.ProjectPhaseHistories.AddAsync(historyEntry, cancellationToken);
    }

}

public interface IProjectPhaseHistoryRepository
{
    Task<ProjectPhaseHistory?> GetLastForProjectAsync(
        Guid projectId,
        CancellationToken cancellationToken);

    Task AddAsync(
        ProjectPhaseHistory historyEntry,
        CancellationToken cancellationToken);
}