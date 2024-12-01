using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Tickets;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Tickets;
public class UpdateTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPut("/{id}", HandleAsync)
    .WithName("Ticket: Update")
    .WithSummary("Atualiza um Ticket")
    .WithDescription("Atualiza um Ticket")
    .WithOrder(2)
    .Produces<Response<Ticket?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITicketHandler handler,
        UpdateTicketRequest request,
        long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;

        var result = await handler.UpdateAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}