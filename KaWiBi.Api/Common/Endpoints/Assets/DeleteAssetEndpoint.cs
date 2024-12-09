using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Asset;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Assets;
public class DeleteAssetEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id}", HandleAsync)
    .WithName("Asset: Delete")
    .WithSummary("Exclui um Objeto")
    .WithDescription("Exclui um Objeto")
    .WithOrder(3)
    .Produces<Response<Asset?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IAssetHandler handler,
        long id)
    {
        var request =  new DeleteAssetRequest{
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
