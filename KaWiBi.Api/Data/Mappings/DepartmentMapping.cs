using KaWiBi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaWiBi.Api.Data.Mappings;
public class DepartmentMapping : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
         builder.ToTable("Department");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(180)
            .HasColumnType("NVARCHAR");

        builder.Property(d => d.Description)
            .HasMaxLength(255)
            .HasColumnType("NVARCHAR");

        builder.Property(d => d.Location)
            .HasMaxLength(255)
            .HasColumnType("NVARCHAR");

        builder.Property(d => d.Lat)
            .HasColumnType("DECIMAL(9,6)")
            .IsRequired(false);

        builder.Property(d => d.Lon)
            .HasColumnType("DECIMAL(9,6)")
            .IsRequired(false);;

        builder.HasMany(d => d.Assets)
            .WithOne(a => a.Department)
            .HasForeignKey(a => a.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}