using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public DbSet<PipelineProject> Projects => Set<PipelineProject>();
    public DbSet<ProjectPhaseHistory> ProjectPhaseHistories { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Phase> Phases => Set<Phase>();


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Server=coinbelsh.postgres.database.azure.com;Database=projectmanagment;Port=5432;User Id=Laconics;Password=McKresha2024!;Ssl Mode=Require");
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
