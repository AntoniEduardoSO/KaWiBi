using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Notes;
using KaWiBi.Core.Responses;

namespace KaWiBi.Core.Handlers;
public interface INoteHandler
{
    Task<Response<Note>> CreateAsync(CreateNoteRequest request);
    Task<Response<Note>> UpdateAsync(UpdateNoteRequest request);
    Task<Response<Note>> DeleteAsync(DeleteNoteRequest request);
    Task<Response<Note>> GetByIdAsync(GetNoteByIdRequest request);
    Task<Response<List<Note>>> GetByTicketAsync(GetAllNoteByTicketRequest request);
}