using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Models
{
    public class TicketDto
    {
        public long Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ETicketStatus Status { get; set; } = ETicketStatus.Newer;
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}