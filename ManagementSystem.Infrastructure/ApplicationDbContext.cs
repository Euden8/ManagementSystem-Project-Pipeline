using ManagementSystem.Domain;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public DbSet<PipelineProject> Projects => Set<PipelineProject>();
    public DbSet<ProjectPhaseHistory> ProjectPhaseHistories => Set<ProjectPhaseHistory>();
    public DbSet<Attachment> Attachments => Set<Attachment>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}