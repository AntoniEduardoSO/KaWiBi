using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Requests.Department;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Departments;
public class UpdateDepartmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPut("/{id}", HandleAsync)
    .WithName("Department: Update")
    .WithSummary("Atualiza um Local")
    .WithDescription("Atualiza um Local")
    .WithOrder(2)
    .Produces<Response<Core.Models.Department?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IDepartmentHandler handler,
        UpdateDepartmentRequest request,
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