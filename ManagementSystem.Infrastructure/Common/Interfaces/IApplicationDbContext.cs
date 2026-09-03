using ManagementSystem.Domain;
using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<PipelineProject> Projects { get; }
    DbSet<Phase> Phases { get; }
    DbSet<ProjectPhaseHistory> ProjectPhaseHistories { get; }
    DbSet<Attachment> Attachments { get; }
    DbSet<PhaseAuditLog> PhaseAuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
