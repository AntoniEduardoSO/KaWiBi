using KaWiBi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaWiBi.Api.Data.Mappings;
public class TicketMapping : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Ticket");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.UserId)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        builder.Property(t => t.Status)
            .IsRequired()
            .HasColumnType("SMALLINT");

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .IsRequired();

        builder.Property(t => t.FinishedAt)
            .IsRequired(false);

        builder.HasMany(t => t.Notes)
            .WithOne(n => n.Ticket)
            .HasForeignKey(n => n.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.AssetId)
            .IsRequired(false);

        builder.Property(t => t.DepartmentOwner)
            .IsRequired();
        
        builder.Property(t => t.DepartmentToExecute)
            .IsRequired();
        
        builder.Property(t => t.Owner)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        builder.Property(t => t.Executer)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);
    }
}