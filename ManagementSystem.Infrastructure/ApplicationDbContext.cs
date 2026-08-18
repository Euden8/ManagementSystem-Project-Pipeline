using ManagementSystem.Domain;
using ManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<PipelineProject> Projects => Set<PipelineProject>();
    public DbSet<Phase> Phases => Set<Phase>();

    public DbSet<ProjectPhaseHistory> ProjectPhaseHistories { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    
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