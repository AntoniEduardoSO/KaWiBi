using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Tickets;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Tickets;
public class DeleteTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id}", HandleAsync)
    .WithName("Ticket: Delete")
    .WithSummary("Exclui um Ticket")
    .WithDescription("Exclui um Ticket")
    .WithOrder(3)
    .Produces<Response<Ticket?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITicketHandler handler,
        long id)
    {
        var request =  new DeleteTicketRequest{
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        

        var result = await handler.DeleteAsync(request);
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}