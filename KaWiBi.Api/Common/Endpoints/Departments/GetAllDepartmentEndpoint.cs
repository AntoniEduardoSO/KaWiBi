using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Requests.Department;
using KaWiBi.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KaWiBi.Api.Common.Endpoints.Departments;
public class GetAllDepartmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
    .WithName("Department: Get All")
    .WithSummary("Recupera todos os Locais")
    .WithDescription("Recupera todos os Locais")
    .WithOrder(5)
    .Produces<PagedResponse<Core.Models.Department?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IDepartmentHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
    {
        var request = new GetAllDepartmentRequest
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