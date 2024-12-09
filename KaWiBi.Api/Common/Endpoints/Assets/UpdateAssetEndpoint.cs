using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Asset;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Assets;
public class UpdateAssetEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPut("/{id}", HandleAsync)
    .WithName("Asset: Update")
    .WithSummary("Atualiza um Objeto")
    .WithDescription("Atualiza um Objeto")
    .WithOrder(2)
    .Produces<Response<Asset?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IAssetHandler handler,
        UpdateAssetRequest request,
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
