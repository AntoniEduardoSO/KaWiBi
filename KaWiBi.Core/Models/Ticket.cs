using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Models;
public class Ticket
{
    public long Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    [JsonIgnore]
    public List<Note>? Notes { get; set; } = [];

    public ETicketStatus Status { get; set; } = ETicketStatus.Newer;
    public string Title { get; set; } = string.Empty;
    public string Description {get;set;} = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public DateTime? FinishedAt {get;set;}

    public DateTime EstimatedTimeToFinish { get; set; }

    public long? AssetId { get; set; }
    public long DepartmentOwner { get; set; }
    public long DepartmentToExecute { get; set; }
    public string? Executer { get; set; } = string.Empty;

    [NotMapped]
    public Asset? Asset { get; set; }
    [NotMapped]
    public Department Department { get; set; } = null!;
}