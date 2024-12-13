using System.Text.Json.Serialization;
using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Models;
public class Note
{
    public long Id { get; set; }

    public string UserId {get;set;} = string.Empty;
    public ENoteCategory Status  { get; set; } = ENoteCategory.Accompaniment;

    public long TicketId {get;set;}
    [JsonIgnore]
    public Ticket Ticket { get; set; } = null!;

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}