using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Requests.Tickets;
public class UpdateTicketRequest : Request
{
    public long Id { get; set; }
    public ETicketStatus Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description {get;set;} = string.Empty;
}