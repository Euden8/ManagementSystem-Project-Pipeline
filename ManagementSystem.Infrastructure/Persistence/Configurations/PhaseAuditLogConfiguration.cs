using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Infrastructure.Persistence.Configurations;

public class PhaseAuditLogConfiguration : IEntityTypeConfiguration<PipelineProject>
{
    public void Configure(EntityTypeBuilder<PhaseAuditLog> builder)
    {
        builder.ToTable("PhaseAuditLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Action)
            .isRequired()
            .HasMaxLength(100);
 
        builder.Property(x => x.ChangedByUserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(x => x.ChangedAt)
            .IsRequired();

        builder.Property(x => x.OldValues)
            .HasColumnType("jsonb");

        builder.Property(x => x.NewValues)
            .HasColumnType("jsonb");   

        builder.HasOne(x => x.Phase)
            .WithMany()
            .HasForeignKey(x => x.PhaseId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(x => x.PhaseId);
    }
}