using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Infrastructure.Persistence.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.FileName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.ContentType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.StorageKey)
            .HasMaxLength(500);

        builder.Property(a => a.ExternalUrl)
            .HasMaxLength(2048);

        builder.Property(a => a.Caption)
            .HasMaxLength(500);


        builder.Property(a => a.Kind)
            .HasConversion<string>() //ruan enums si nje string ne vend qe te ruhet si "Document" ose "Link"
            .HasMaxLength(50);


        builder.HasQueryFilter(a => !a.IsDeleted);// Soft delete filter


        builder.HasOne(a => a.Project)
            .WithMany()
            .HasForeignKey(a => a.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);// lidh attachment me project dhe kur fshihet project, fshihet edhe attachment

        builder.HasOne(a => a.UploadedByUser)
            .WithMany()
            .HasForeignKey(a => a.UploadedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}