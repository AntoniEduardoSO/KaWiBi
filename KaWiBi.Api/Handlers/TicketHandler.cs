using KaWiBi.Api.Data;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Tickets;
using KaWiBi.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace KaWiBi.Api.Handlers;
public class TicketHandler(AppDbContext context) : ITicketHandler
{
    public async Task<string> FindDepartmentName(long id)
    {
        var department = await context.Departments.FirstOrDefaultAsync(x => x.Id == id);

        return department is null
            ? string.Empty
            : department.Name;
    }
    public async Task<string> AssignTicketAuthor(CreateTicketRequest request)
    {
        var userMain = await context
            .Users
            .FirstOrDefaultAsync(x => x.UserName == request.UserId);

        if (userMain == null)
            return string.Empty;

        if (userMain.DepartmentId != request.DepartmentToExecute)
        {
            DateTime now = DateTime.Now;
            TimeSpan currentTime = now.TimeOfDay;

            var count = await context
                .Users
                .Where
                (
                    u => u.DepartmentId == request.DepartmentToExecute
                    && currentTime >= u.StartTime
                    && currentTime <= u.EndTime
                )
                .CountAsync();

            if (count == 0)
                return string.Empty;

            var randomIndex = new Random().Next(count);

            var randomAssignee = await context.Users
            .Where(u => u.DepartmentId == request.DepartmentToExecute
                && currentTime >= u.StartTime
                && currentTime <= u.EndTime)
            .Skip(randomIndex)
            .FirstOrDefaultAsync();

            if (randomAssignee != null)
                return randomAssignee.UserName ?? string.Empty;

            return string.Empty;
        }

        else
        {
            return request.Executer = request.UserId;
        }
    }
    
    public async Task<PagedResponse<List<TicketDto>>> FilterAsync(TicketFilterRequest request)
    {
        IQueryable<Ticket> query = context.Tickets
            .AsNoTracking()
            .OrderByDescending(x => x.UpdatedAt);


        if (!string.IsNullOrEmpty(request.Title))
        {
            query = query.Where(t => EF.Functions.Like(t.Title, $"%{request.Title}%"));
        }

        if (!string.IsNullOrEmpty(request.UserId))
        {
            query = query.Where(t => EF.Functions.Like(t.UserId, $"{request.UserId}%"));
        }

        if (request.Status != null)
        {
            query = query.Where(t => t.Status == request.Status);
        }

        if (!string.IsNullOrEmpty(request.Executer))
        {
            query = query.Where(t => EF.Functions.Like(t.Executer, $"{request.Executer}%"));
        }

        if (request.AssetId != null)
        {
            query = query.Where(t => t.AssetId == request.AssetId);
        }

        if (request.DepartmentToExecute != null)
        {
            query = query.Where(t => t.DepartmentToExecute == request.DepartmentToExecute);
        }

        if (request.DepartmentOwner != null)
        {
            query = query.Where(t => t.DepartmentOwner == request.DepartmentOwner);
        }

        var count = await query.CountAsync();
        var ticketEntities = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var tickets = new List<TicketDto>();
        foreach (var ticket in ticketEntities)
        {
            tickets.Add(new TicketDto
            {
                Id = ticket.Id,
                UserId = ticket.UserId,
                Status = ticket.Status,
                Title = ticket.Title,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt,
                DepartmentOwner = await FindDepartmentName(ticket.DepartmentOwner),
                DepartmentToExecute = await FindDepartmentName(ticket.DepartmentToExecute),
                Executer = ticket.Executer ?? string.Empty
            });
        }

        return new PagedResponse<List<TicketDto>>(
            tickets,
            count,
            request.PageNumber,
            request.PageSize
        );
    }

    public async Task<Response<Ticket>> CreateAsync(CreateTicketRequest request)
    {
        try
        {
            request.Executer = await AssignTicketAuthor(request);

            var ticket = new Ticket
            {
                UserId = request.UserId,
                Status = request.Status,
                Description = request.Description,
                Title = request.Title,
                CreatedAt = DateTime.Now,
                DepartmentOwner = request.DepartmentOwner,
                DepartmentToExecute = request.DepartmentToExecute,
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

            var ticketEntities = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var tickets = new List<TicketDto>();
            foreach (var ticket in ticketEntities)
            {
                tickets.Add(new TicketDto
                {
                    Id = ticket.Id,
                    UserId = ticket.UserId,
                    Status = ticket.Status,
                    Title = ticket.Title,
                    CreatedAt = ticket.CreatedAt,
                    UpdatedAt = ticket.UpdatedAt,
                    DepartmentOwner = await FindDepartmentName(ticket.DepartmentOwner),
                    DepartmentToExecute = await FindDepartmentName(ticket.DepartmentToExecute),
                    Executer = ticket.Executer ?? string.Empty
                });
            }

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
            .Where(t => t.FinishedAt != null)
            .ToListAsync();

        if (completedTickets.Count == 0)
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