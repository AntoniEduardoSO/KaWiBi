using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Tickets;
using KaWiBi.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KaWiBi.Api.Common.Endpoints.Tickets;
public class GetAllTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
    .WithName("Ticket: Get All")
    .WithSummary("Recupera todos os Ticket")
    .WithDescription("Recupera todos os Tickets")
    .WithOrder(5)
    .Produces<Response<Ticket?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITicketHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
    {
        var request = new GetAllTicketRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.GetAllAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }

}