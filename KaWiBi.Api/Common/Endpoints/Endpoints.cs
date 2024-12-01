using KaWiBi.Api.Common.Api;
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
            .MapEndpoint<GetRolesEndpoint>();
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