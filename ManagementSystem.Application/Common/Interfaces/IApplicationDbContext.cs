using Microsoft.EntityFrameworkCore;
using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<PipelineProject> PipelineProjects { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}