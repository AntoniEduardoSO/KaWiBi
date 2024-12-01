using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Tickets;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Tickets;
public class GetByIdTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/{id}", HandleAsync)
    .WithName("Ticket: Get By Id")
    .WithSummary("Recupera um Ticket pelo Id")
    .WithDescription("Recupera um Ticket pelo Id")
    .WithOrder(4)
    .Produces<Response<Ticket?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITicketHandler handler,
        long id)
    {
        var request = new GetTicketByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}