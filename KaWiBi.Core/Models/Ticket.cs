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

    [NotMapped]
    public List<long> NoteIds { get; set; } = [];

    public ETicketStatus Status { get; set; } = ETicketStatus.Newer;
    public string Title { get; set; } = string.Empty;
    public string Description {get;set;} = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }
}