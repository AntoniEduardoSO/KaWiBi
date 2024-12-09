using System.Security.Claims;
using Azure;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Tickets;

namespace KaWiBi.Api.Common.Endpoints.Tickets;
public class CreateTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("/", HandleAsync)
    .WithName("Ticket: Create")
    .WithSummary("Cria um novo Ticket")
    .WithDescription("Cria um novo Ticket")
    .WithOrder(1)
    .Produces<Response<Ticket?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITicketHandler handler, 
        CreateTicketRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Owner = user.Identity?.Name ?? string.Empty;
        request.Executer = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);

        return result.IsSuccess 
            ? TypedResults.Created($"{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}