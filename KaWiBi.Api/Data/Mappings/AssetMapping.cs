using KaWiBi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaWiBi.Api.Data.Mappings;

public class AssetMapping : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("Asset");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name)
        .IsRequired()
        .HasMaxLength(180)
        .HasColumnType("NVARCHAR");

        builder.Property(o => o.SerialNumber)
        .IsRequired()
        .HasMaxLength(255)
        .HasColumnType("NVARCHAR");

        builder.Property(o => o.Stamp)
        .IsRequired()
        .HasMaxLength(255)
        .HasColumnType("NVARCHAR");

        builder.Property(o => o.Pattern)
        .IsRequired()
        .HasMaxLength(255)
        .HasColumnType("NVARCHAR");

        builder.Property(o => o.IpAddress)
        .IsRequired(false)
        .HasColumnType("VARBINARY(16)");

        builder.HasOne(a => a.Department)
        .WithMany(d => d.Assets)
        .HasForeignKey(a => a.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict)
        .IsRequired();
    }
}