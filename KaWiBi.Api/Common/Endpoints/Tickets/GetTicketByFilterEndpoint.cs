using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core;
using KaWiBi.Core.Enums;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Tickets;
using KaWiBi.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KaWiBi.Api.Common.Endpoints.Tickets;
public class GetTicketByFilterEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/filter", HandleAsync)
    .WithName("Ticket: Get By Filter")
    .WithSummary("Recupera todos os Ticket por um filtro")
    .WithDescription("Recupera todos os Tickets por um filtro")
    .WithOrder(6)
    .Produces<PagedResponse<List<TicketDto?>>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITicketHandler handler,
        [FromQuery] ETicketStatus? statusid,
        [FromQuery] string? title,
        [FromQuery] long? assetId,
        [FromQuery] string? executer,
        [FromQuery] long? departmentOwner,
        [FromQuery] long? departmentToExecute,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
    {
        var request = new TicketFilterRequest
        {
            Status = statusid,
            Title = title,
            AssetId = assetId,
            Executer = executer,
            DepartmentOwner = departmentOwner,
            DepartmentToExecute = departmentToExecute,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        

        var result = await handler.FilterAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}