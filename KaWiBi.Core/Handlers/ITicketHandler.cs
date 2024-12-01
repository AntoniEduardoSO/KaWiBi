using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Notes;
using KaWiBi.Core.Requests.Tickets;
using KaWiBi.Core.Responses;

namespace KaWiBi.Core.Handlers;
public interface ITicketHandler
{
    Task<Response<Ticket>> CreateAsync(CreateTicketRequest request);
    Task<Response<Ticket>> UpdateAsync(UpdateTicketRequest request);
    Task<Response<Ticket>> DeleteAsync(DeleteTicketRequest request);
    Task<Response<Ticket>> GetByIdAsync(GetTicketByIdRequest request);
    Task<PagedResponse<List<TicketDto>>> GetAllAsync(GetAllTicketRequest request);
}