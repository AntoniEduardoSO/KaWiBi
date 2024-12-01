namespace KaWiBi.Core.Requests.Notes;
public class GetAllNoteByTicketRequest : PagedRequest
{
    public long TicketId { get; set; }
}