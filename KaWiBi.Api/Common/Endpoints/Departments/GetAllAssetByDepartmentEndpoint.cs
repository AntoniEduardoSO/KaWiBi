using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Department;
using KaWiBi.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KaWiBi.Api.Common.Endpoints.Departments;
public class GetAllAssetByDepartmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
=> app.MapGet("/{id}/asset/", HandleAsync)
.WithName("Asset: Get All By Department")
.WithSummary("Recupera todos os Objetos pelo Local")
.WithDescription("Recupera todos os Obejtos pelo Local")
.WithOrder(5)
.Produces<PagedResponse<Asset?>>();


    private static async Task<IResult> HandleAsync(
        long id,
        ClaimsPrincipal user,
        IDepartmentHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
    {
        var request = new GetAllAssetByDepartmentRequest
        {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.GetAllAssetByDeparmentAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}