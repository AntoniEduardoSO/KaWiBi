using KaWiBi.Api.Data;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Notes;
using KaWiBi.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace KaWiBi.Api.Handlers;
public class NoteHandler(AppDbContext context) : INoteHandler
{
    public async Task<Response<Note>> CreateAsync(CreateNoteRequest request)
    {
        try
        {
            var note = new Note
            {
                UserId = request.UserId,
                TicketId = request.TicketId,
                Content = request.Content,
                CreatedAt = DateTime.Now
            };

            await context.Notes.AddAsync(note);

            var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == request.TicketId);

            if (ticket is not null){
                ticket.UpdatedAt = note.CreatedAt;
                // context.Entry(ticket).Property(t => t.UpdatedAt).IsModified = true;
            }
            else
                return new Response<Note>(null, 404, "Ticket não encontrado");
            

            await context.SaveChangesAsync();

            return new Response<Note>(note, 201, "Comentário criado com sucesso");
        }
        catch 
        {
            // Log de erro (ex)
            return new Response<Note>(null, 500, "Não foi possível criar o comentário");
        }
    }


    public async Task<Response<Note>> DeleteAsync(DeleteNoteRequest request)
    {
        try
        {
            var note = await context
                .Notes
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (note is null)
                return new Response<Note>(null, 404, "Comentario nao encontrado");
            

            context.Notes.Remove(note);
            await context.SaveChangesAsync();

            return new Response<Note>(note, message: "Comentario excluido com sucesso!");
        }
        catch
        {
            return new Response<Note>(null, 500, "Nao foi possivel excluir o comentario");
        }

    }

    public async Task<Response<Note>> GetByIdAsync(GetNoteByIdRequest request)
    {
        try
        {
            var note = await context
                    .Notes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.TicketId == request.TicketId);

            return note is null
                ? new Response<Note>(null, 404, "Comentario nao encontrado")
                : new Response<Note>(note);
        }
        catch
        {
            return new Response<Note>(null, 500, "Nao foi possivel recuperar o comentario.");
        }
    }

    public async Task<Response<List<Note>>> GetByTicketAsync(GetAllNoteByTicketRequest request)
    {
        try
        {
            List<Note> notes = await context
                .Notes
                .AsNoTracking()
                .Where(x => x.TicketId == request.TicketId)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();

            if (notes.Count <= 0)
            {
                return new Response<List<Note>>(null, 404, "Ticket sem comentarios!");
            }

            return new Response<List<Note>>(notes);
        }
        catch
        {
            return new Response<List<Note>>(null, 500, "Nao foi possivel recuperar os comentarios");
        }
    }

    public async Task<Response<Note>> UpdateAsync(UpdateNoteRequest request)
    {
        try
        {
            var note = await context
                        .Notes
                        .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (note == null)
                return new Response<Note>(null, 404, "Comentario nao encontrado");


            note.UpdatedAt = DateTime.Now;
            note.Content = request.Content;

            var ticket = await context
                .Tickets
                .FirstOrDefaultAsync(x => x.Id == request.TicketId);

            if(ticket is null)
                return new Response<Note>(null,500, "Nao foi possivel recupar o ticket");
            
            ticket.UpdatedAt = DateTime.Now;

            context.Notes.Update(note);
            await context.SaveChangesAsync();

            return new Response<Note>(note, message: "Comentario atualizado com sucesso");
        }
        catch
        {
            return new Response<Note>(null, 500, "Nao foi possivel atualizar o comentario");
        }
    }
}