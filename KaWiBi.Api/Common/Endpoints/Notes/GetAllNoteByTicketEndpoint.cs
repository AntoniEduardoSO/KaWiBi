using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Requests.Notes;

namespace KaWiBi.Api.Common.Endpoints.Notes;
public class GetAllNoteByTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
    .WithName("Notes: Get All By Ticket")
    .WithSummary("Recupera todos os comentarios do Ticket")
    .WithDescription("Recupera todos os comentarios do Ticket")
    .WithOrder(5);

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        INoteHandler handler, 
        long ticketid)
    {
        var request =  new GetAllNoteByTicketRequest{
            UserId = user.Identity?.Name ?? string.Empty,
            TicketId = ticketid,
        };
        
        var result = await handler.GetByTicketAsync(request);

        return result.IsSuccess 
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}