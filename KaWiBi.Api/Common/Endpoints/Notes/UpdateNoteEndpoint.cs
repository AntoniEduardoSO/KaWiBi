using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Notes;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Notes;
public class UpdateNoteEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPut("/{id}", HandleAsync)
    .WithName("Notes: Update")
    .WithSummary("Atualiza um no Ticket")
    .WithDescription("Atualiza comentario no Ticket")
    .WithOrder(2)
    .Produces<Response<Note?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        INoteHandler handler, 
        UpdateNoteRequest request,
        long ticketid,
        long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.TicketId = ticketid;
        request.Id = id;
        
        var result = await handler.UpdateAsync(request);

        return result.IsSuccess 
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}