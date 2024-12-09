using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Requests.Department;
using KaWiBi.Core.Responses;

namespace KaWiBi.Api.Common.Endpoints.Departments;
public class DeleteDepartmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id}", HandleAsync)
    .WithName("Department: Delete")
    .WithSummary("Exclui um Local")
    .WithDescription("Exclui um Local")
    .WithOrder(3)
    .Produces<Response<Core.Models.Department?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IDepartmentHandler handler,
        long id)
    {
        var request =  new DeleteDepartmentRequest{
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}