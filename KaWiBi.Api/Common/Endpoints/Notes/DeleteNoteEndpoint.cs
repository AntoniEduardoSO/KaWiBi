using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Notes;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Notes;
public class DeleteNoteEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id}", HandleAsync)
    .WithName("Notes: Delete")
    .WithSummary("Deleta um comentario no Ticket")
    .WithDescription("Deleta um comentario no Ticket")
    .WithOrder(3)
    .Produces<Response<Note?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        INoteHandler handler, 
        long ticketid,
        long id)
    {
        var request =  new DeleteNoteRequest{
            UserId = user.Identity?.Name ?? string.Empty,
            TicketId = ticketid,
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);

        return result.IsSuccess 
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}