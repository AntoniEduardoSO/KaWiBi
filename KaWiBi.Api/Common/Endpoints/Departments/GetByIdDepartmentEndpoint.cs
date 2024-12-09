using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Requests.Department;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Departments;
public class GetByIdDepartmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/{id}", HandleAsync)
    .WithName("Department: Get By Id")
    .WithSummary("Recupera um Local pelo Id")
    .WithDescription("Recupera um Local pelo Id")
    .WithOrder(4)
    .Produces<Response<Core.Models.Department?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IDepartmentHandler handler,
        long id)
    {
        var request = new GetDepartmentByIdRequest
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