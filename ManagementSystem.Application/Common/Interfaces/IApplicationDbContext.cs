using ManagementSystem.Domain;
using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Attachment> Attachments { get; }
    DbSet<PipelineProject> Projects { get; }
    DbSet<ProjectPhaseHistory> ProjectPhaseHistories { get; }
    DbSet<Phase> Phases { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}