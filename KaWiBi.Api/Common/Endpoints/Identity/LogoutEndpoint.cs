using KaWiBi.Api.Common.Api;
using KaWiBi.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace KaWiBi.Api.Common.Endpoints.Identity;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        =>  app
            .MapPost("/logout", HandleAsync)
            .RequireAuthorization()
            .WithName("Identity: Logout")
            .WithSummary("Deslogando o usuario")
            .WithDescription("Deslogando o usuario");
             
    private static async Task<IResult> HandleAsync(
        SignInManager<User> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
}
