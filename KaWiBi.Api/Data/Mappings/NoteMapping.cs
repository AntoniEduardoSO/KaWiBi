using KaWiBi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaWiBi.Api.Data.Mappings
{
    public class NoteMapping : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Note");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.UserId)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

            builder.Property(t => t.Content)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

            builder.Property(t => t.CreatedAt)
            .IsRequired();

            builder.Property(t => t.UpdatedAt)
            .IsRequired(false);

            builder.HasOne(n => n.Ticket)
                .WithMany(t => t.Notes)
                .HasForeignKey(n => n.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}