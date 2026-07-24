using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Infrastructure.Persistence.Configurations;

public class PhaseConfiguration : IEntityTypeConfiguration<Phase>
{
    public void Configure(EntityTypeBuilder<Phase> builder)
    {
        builder.ToTable("Phases");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Order)
            .IsRequired();

        builder.HasIndex(x => x.Order)
            .IsUnique();
    }
}