using KaWiBi.Api.Common.Api;
using KaWiBi.Api.Common.Endpoints.Assets;
using KaWiBi.Api.Common.Endpoints.Departments;
using KaWiBi.Api.Common.Endpoints.Identity;
using KaWiBi.Api.Common.Endpoints.Notes;
using KaWiBi.Api.Common.Endpoints.Tickets;
using KaWiBi.Api.Models;

namespace KaWiBi.Api.Common.Endpoints;
public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("")
            .WithTags("Health Check")
            .MapGet("/", () => new {message= "Ok"});

        endpoints.MapGroup("v1/tickets")
            .WithTags("Tickets")
            .RequireAuthorization()
            .MapEndpoint<CreateTicketEndpoint>()
            .MapEndpoint<UpdateTicketEndpoint>()
            .MapEndpoint<DeleteTicketEndpoint>()
            .MapEndpoint<GetTicketByFilterEndpoint>()
            .MapEndpoint<GetByIdTicketEndpoint>()
            .MapEndpoint<GetAllTicketEndpoint>();

        endpoints.MapGroup("v1/tickets/{ticketid}/notes")
            .WithTags("Notes")
            .RequireAuthorization()
            .MapEndpoint<CreateNoteEndpoint>()
            .MapEndpoint<UpdateNoteEndpoint>()
            .MapEndpoint<DeleteNoteEndpoint>()
            .MapEndpoint<GetNoteByIdEndpoint>()
            .MapEndpoint<GetAllNoteByTicketEndpoint>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>()
            .MapEndpoint<RegisterEndpoint>();

        endpoints.MapGroup("v1/department")
            .WithTags("Department")
            .MapEndpoint<CreateDepartmentEndpoint>()
            .MapEndpoint<UpdateDepartmentEndpoint>()
            .MapEndpoint<DeleteDepartmentEndpoint>()
            .MapEndpoint<GetByIdDepartmentEndpoint>()
            .MapEndpoint<GetAllDepartmentEndpoint>()
            .MapEndpoint<GetAllAssetByDepartmentEndpoint>();

        endpoints.MapGroup("v1/asset")
            .WithTags("Asset")
            .MapEndpoint<CreateAssetEndpoint>()
            .MapEndpoint<UpdateAssetEndpoint>()
            .MapEndpoint<DeleteAssetEndpoint>()
            .MapEndpoint<GetByIdAssetEndpoint>()
            .MapEndpoint<GetAllAssetEndpoint>();

    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
     where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);

        return app;
    }
}

// Muito importante utilizar o "this" com "Webapplication"
// pois permite utilizar metodos de extensao dentro do app do proprio .net API
// vale ressaltar, precisa a classe ser static para isso funcionar!