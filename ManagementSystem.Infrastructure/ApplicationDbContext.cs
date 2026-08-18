using ManagementSystem.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Domain.Entities;

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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PipelineManagementDb;Username=postgres;Password=euden123");
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
        typeof(ApplicationDbContext).Assembly);
    }
}
