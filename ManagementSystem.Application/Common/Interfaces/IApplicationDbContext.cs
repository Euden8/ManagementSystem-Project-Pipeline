namespace ManagementSystem.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;
using ManagementSystem.Domain.Entities;

public interface IApplicationDbContext
{
    DbSet<PipelineProject> Projects { get; }
    DbSet<ProjectPhaseHistory> ProjectPhaseHistories { get; }
    DbSet<Attachment> Attachments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}