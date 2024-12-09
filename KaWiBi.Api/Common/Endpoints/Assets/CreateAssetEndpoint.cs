using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Asset;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Assets;
public class CreateAssetEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("/", HandleAsync)
    .WithName("Asset: Create")
    .WithSummary("Cria um novo Objeto")
    .WithDescription("Cria um novo Objeto")
    .WithOrder(1)
    .Produces<Response<Asset?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IAssetHandler handler, 
        CreateAssetRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);

        return result.IsSuccess 
            ? TypedResults.Created($"{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}