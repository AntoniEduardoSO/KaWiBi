using KaWiBi.Api.Data;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Tickets;
using KaWiBi.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace KaWiBi.Api.Handlers;
public class TicketHandler(AppDbContext context) : ITicketHandler
{
    public async Task<Response<Ticket>> CreateAsync(CreateTicketRequest request)
    {
        try
        {
            var ticket = new Ticket
            {
                UserId = request.UserId,
                Description = request.Description,
                Title = request.Title,
                CreatedAt = DateTime.Now,
                DepartmentOwner = request.DepartmentOwner,
                DepartmentToExecute = request.DepartmentToExecute,
                Owner = request.Owner,
                Executer = request.Executer,
                AssetId = request.AssetId ?? null,
            };

            ticket.EstimatedTimeToFinish = await GetEstimatedFinishTimeAsync(ticket);


            await context.Tickets.AddAsync(ticket);
            await context.SaveChangesAsync();

            return new Response<Ticket>(ticket, 201, "Ticket criado com sucesso!");
        }
        catch
        {
            // Log de erro (ex)
            return new Response<Ticket>(null, 500, "Não foi possível criar o ticket");
        }
    }

    public async Task<Response<Ticket>> DeleteAsync(DeleteTicketRequest request)
    {
        try
        {
            var ticket = await context
                .Tickets
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (ticket is null)
                return new Response<Ticket>(null, 404, "Ticket nao encontrado");


            context.Tickets.Remove(ticket);
            await context.SaveChangesAsync();

            return new Response<Ticket>(ticket, message: "Comentario excluido com sucesso!");
        }
        catch
        {
            return new Response<Ticket>(null, 500, "Nao foi possivel excluir o ticket");
        }
    }

    public async Task<PagedResponse<List<TicketDto>>> GetAllAsync(GetAllTicketRequest request)
    {
        try
        {
            var query = context
                .Tickets
                .AsNoTracking()
                .OrderByDescending(x => x.UpdatedAt);

            var tickets = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(ticket => new TicketDto
                {
                    Id = ticket.Id,
                    UserId = ticket.UserId,
                    Status = ticket.Status,
                    Title = ticket.Title,
                    CreatedAt = ticket.CreatedAt,
                    UpdatedAt = DateTime.Now
                })
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<TicketDto>>(
                tickets,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch
        {
            return new PagedResponse<List<TicketDto>>(null, 500, "Nao foi possivel consultar os tickets");
        }
    }

    public async Task<Response<Ticket>> GetByIdAsync(GetTicketByIdRequest request)
    {
        try
        {
            var ticket = await context
                    .Tickets
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

            return ticket is null
                ? new Response<Ticket>(null, 404, "Comentario nao encontrado")
                : new Response<Ticket>(ticket);
        }
        catch
        {
            return new Response<Ticket>(null, 500, "Nao foi possivel recuperar o comentario.");
        }
    }

    public async Task<DateTime> GetEstimatedFinishTimeAsync(Ticket ticket)
    {
        var completedTickets = await context.Tickets
            .Where(t => t.FinishedAt !=null)
            .ToListAsync();

        if(completedTickets.Count == 0)
            return ticket.CreatedAt.AddHours(3);

        var averageTime = completedTickets.Average(t => (t.FinishedAt!.Value - t.CreatedAt).TotalHours);

        return ticket.CreatedAt.AddHours(averageTime);
    }

    public async Task<Response<Ticket>> UpdateAsync(UpdateTicketRequest request)
    {
        try
        {
            var ticket = await context
                .Tickets
                .FirstOrDefaultAsync(x => x.Id == request.Id
                && x.UserId == x.UserId);

            if (ticket is null)
                return new Response<Ticket>(null, 404, "Ticket nao encontrado!");

            ticket.Status = request.Status;
            ticket.Title = request.Title;
            ticket.Description = request.Description;
            ticket.UpdatedAt = DateTime.Now;
            ticket.AssetId = request.AssetId ?? null;
            ticket.DepartmentOwner = request.DepartmentOwner;
            ticket.DepartmentToExecute = request.DepartmentToExecute;
            ticket.Owner = request.Owner;
            ticket.Executer = request.Executer;

            context.Tickets.Update(ticket);
            await context.SaveChangesAsync();


            return new Response<Ticket>(ticket, message: "Ticket atualizado com sucesso!");
        }
        catch
        {
            return new Response<Ticket>(null, 500, "Nao foi possivel atualizar o ticket!");
        }
    }
}