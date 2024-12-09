using Azure;
using KaWiBi.Api.Common.Api;
using KaWiBi.Api.Models;
using KaWiBi.Core.Requests.Identity;
using KaWiBi.Core.Responses.Identity;
using Microsoft.AspNetCore.Identity;

namespace KaWiBi.Api.Common.Endpoints.Identity;
public class RegisterEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/registrar", HandleAsync)
            .WithName("Registrar novo usuario")
            .WithSummary("Registra um novo usuario")
            .Produces<Response<User>>();

    
    private static async Task<IResult> HandleAsync(
        IdentityRegisterRequest request,
        UserManager<User> userManager
        )
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if(existingUser is not null)
            return Results.Conflict(new {message = "Email ja registrado."});
        
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            DepartmentId = request.DepartmentId,
            Post = request.Post,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if(!result.Succeeded)
        {
            var erroList = result.Errors.Select(e => e.Description).ToList();
            return Results.BadRequest(new {errors = erroList});
        }

        var response = new RegisterResponse
        {
            UserId = user.Id,
            Email = user.Email,
            FullName = "Antonio Eduardo",
            PhoneNumber = "00 9 1234-5678",
            CreatedAt = DateTime.UtcNow
        };

        return Results.Created($"/v1/identity/users/{user.Id}", response);
    }
}