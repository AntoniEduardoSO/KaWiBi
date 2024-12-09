using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Asset;
using KaWiBi.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KaWiBi.Api.Common.Endpoints.Assets;
public class GetAllAssetEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
    .WithName("Asset: Get All")
    .WithSummary("Recupera todos os Objetos")
    .WithDescription("Recupera todos os Objetos")
    .WithOrder(5)
    .Produces<PagedResponse<Asset?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IAssetHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
    {
        var request = new GetAllAssetRequest
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