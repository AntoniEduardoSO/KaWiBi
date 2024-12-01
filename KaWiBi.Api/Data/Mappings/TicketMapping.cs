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
            .IsRequired(false);

        builder.HasMany(t => t.Notes)
            .WithOne(n => n.Ticket)
            .HasForeignKey(n => n.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}