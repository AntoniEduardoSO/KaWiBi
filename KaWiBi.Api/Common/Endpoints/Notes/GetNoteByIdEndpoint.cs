using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Requests.Notes;

namespace KaWiBi.Api.Common.Endpoints.Notes;
public class GetNoteByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/{id}", HandleAsync)
    .WithName("Notes: Get By Id")
    .WithSummary("Recupera um comentario no Ticket")
    .WithDescription("Recupera um comentario no Ticket")
    .WithOrder(4);


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        INoteHandler handler, 
        long ticketid,
        long id)
    {
        var request =  new GetNoteByIdRequest{
            UserId = user.Identity?.Name ?? string.Empty,
            TicketId = ticketid,
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);

        return result.IsSuccess 
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }

}