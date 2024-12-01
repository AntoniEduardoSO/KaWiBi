using System.Security.Claims;
using KaWiBi.Api.Common.Api;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Requests.Notes;

namespace KaWiBi.Api.Common.Endpoints.Notes;
public class CreateNoteEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("/", HandleAsync)
    .WithName("Notes: Create")
    .WithSummary("Cria um novo comentario no Ticket")
    .WithDescription("Cria um novo comentario no Ticket")
    .WithOrder(1);

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        INoteHandler handler, 
        CreateNoteRequest request,
        long ticketid)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.TicketId = ticketid;
        
        var result = await handler.CreateAsync(request);

        return result.IsSuccess 
            ? TypedResults.Created($"{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}

// Results -> Nao infere o tipo de resposta, baseado no handler
// TypedResults -> Infere o tipo de resposta baseado no retorno do handler.