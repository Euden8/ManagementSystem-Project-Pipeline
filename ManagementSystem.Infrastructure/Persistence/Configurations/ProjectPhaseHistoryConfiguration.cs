using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Infrastructure.Persistence.Configurations;

public class ProjectPhaseHistoryConfiguration : IEntityTypeConfiguration<ProjectPhaseHistory>
{
    public void Configure(EntityTypeBuilder<ProjectPhaseHistory> builder)
    {
        builder.ToTable("ProjectPhaseHistories");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Note)
            .HasMaxLength(1000);
            
        builder.HasOne(h => h.Project)
            .WithMany()
            .HasForeignKey(h => h.ProjectId)
            .OnDelete(DeleteBehavior.Cascade); 
        builder.HasOne(h => h.FromPhase)
            .WithMany()
            .HasForeignKey(h => h.FromPhaseId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(h => h.ToPhase)
            .WithMany()
            .HasForeignKey(h => h.ToPhaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.ChangedByUser)
            .WithMany()
            .HasForeignKey(h => h.ChangedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}