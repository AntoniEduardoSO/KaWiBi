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
        public DateTime? FinishedAt { get; set; }
        public DateTime? EstimatedFinishedAt { get; set; }
        public long DepartmentOwner { get; set; }
        public long DepartmentToExecute { get; set; }
        public string Owner { get; set; } = string.Empty;
        public string Executer { get; set; } = string.Empty;
    }
}