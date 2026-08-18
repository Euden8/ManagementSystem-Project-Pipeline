using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<PipelineProject>
{
    public void Configure(EntityTypeBuilder<PipelineProject> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.OwnerUserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(x => x.PlannedStartDate);
        builder.Property(x => x.PlannedEndDate);
        builder.Property(x => x.ActualStartDate);
        builder.Property(x => x.ActualEndDate);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(x => x.ModifiedBy)
            .HasMaxLength(450);

        builder.Property(x => x.DeletedBy)
            .HasMaxLength(450);

        builder.HasOne(x => x.CurrentPhase)
            .WithMany()
            .HasForeignKey(x => x.CurrentPhaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}