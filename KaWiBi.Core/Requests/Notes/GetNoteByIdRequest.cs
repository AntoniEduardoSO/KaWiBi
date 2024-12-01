namespace KaWiBi.Core.Requests.Notes;
public class GetNoteByIdRequest : Request
{
    public long TicketId { get; set; }
    public long Id { get; set; }
}