using System.Security.Claims;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Requests.Department;
using KaWiBi.Core.Responses;
using KaWiBi.Core.Models;
using KaWiBi.Api.Common.Api;

namespace KaWiBi.Api.Common.Endpoints.Departments;
public class CreateDepartmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("/", HandleAsync)
    .WithName("Department: Create")
    .WithSummary("Cria um novo Local")
    .WithDescription("Cria um novo Local")
    .WithOrder(1)
    .Produces<Response<Core.Models.Department?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IDepartmentHandler handler, 
        CreateDepartmentRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);

        return result.IsSuccess 
            ? TypedResults.Created($"{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}