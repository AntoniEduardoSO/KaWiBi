using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Requests.Tickets;
public class CreateTicketRequest : Request
{
    public ETicketStatus Status { get; set; } = ETicketStatus.Newer;
    public string Title { get; set; } = string.Empty;
    public string Description {get;set;} = string.Empty;
    public long? AssetId  { get; set; }
    public string? Executer { get; set; } = null;
    public long DepartmentOwner { get; set; } 
    public long DepartmentToExecute { get; set; }
}