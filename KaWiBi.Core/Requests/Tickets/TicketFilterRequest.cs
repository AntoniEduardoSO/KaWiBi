using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Requests.Tickets;
public class TicketFilterRequest : PagedRequest
{
    public ETicketStatus? Status { get; set; }
    public string? Title { get; set; } = string.Empty;
    public long? AssetId  { get; set; }
    public string? Executer { get; set; } = null;
    public long? DepartmentOwner { get; set; } 
    public long? DepartmentToExecute { get; set; }
}