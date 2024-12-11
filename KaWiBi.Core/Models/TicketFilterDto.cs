using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Models;
public class TicketFilterDto
{
    public ETicketStatus? Status { get; set; }
    public string? Title { get; set; }
    public long? AssetId { get; set; }
    public string? Executer { get; set; }
    public long? DepartmentOwner { get; set; }
    public long? DepartmentToExecute { get; set; }
    
    // Propriedades de Paginação
    public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;  
}