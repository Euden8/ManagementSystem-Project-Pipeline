using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Application.Common.Interfaces;

public interface IProjectRepository
{
    Task<bool> CodeExistsAsync(
        string code,
        CancellationToken cancellationToken);

    Task<bool> PhaseExistsAsync(
        Guid phaseId,
        CancellationToken cancellationToken);

    Task AddAsync(
        PipelineProject project,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}