using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Asset;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Assets;
public class GetByIdAssetEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
     => app.MapGet("/{id}", HandleAsync)
     .WithName("Asset: Get By Id")
     .WithSummary("Recupera um Objeto pelo Id")
     .WithDescription("Recupera um Objeto pelo Id")
     .WithOrder(4)
     .Produces<Response<Asset?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IAssetHandler handler,
        long id)
    {
        var request = new GetAssetByIdRequest
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