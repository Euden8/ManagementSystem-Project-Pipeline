
using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Infrastructure.Persistence.Repositories;

public sealed class ProjectRepository  : IProjectRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProjectRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> CodeExistsAsync(
        string code,
        CancellationToken cancellationToken)
    {
        return _dbContext.PipelineProjects
            .IgnoreQueryFilters()
            .AnyAsync(
                project => project.Code == code,
                cancellationToken);
    }

    public async Task AddAsync(
        PipelineProject project,
        CancellationToken cancellationToken)
    {
        await _dbContext.PipelineProjects.AddAsync(project, cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

public interface IProjectRepository
{
    Task<bool> CodeExistsAsync(
        string code,
        CancellationToken cancellationToken)
        ;

    Task AddAsync(
        PipelineProject project,
        CancellationToken cancellationToken);
    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}