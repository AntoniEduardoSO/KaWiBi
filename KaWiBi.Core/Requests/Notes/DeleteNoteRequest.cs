namespace KaWiBi.Core.Requests.Notes;
public class DeleteNoteRequest : Request
{
    public long TicketId { get; set; }
    public long Id { get; set; }
}   