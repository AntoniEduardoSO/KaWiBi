using System.ComponentModel.DataAnnotations;
using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Requests.Notes;
public class CreateNoteRequest : Request
{
    public long TicketId { get; set; }
    [Required(ErrorMessage = "Comentario invalido")]
    [MaxLength(80, ErrorMessage = "O comentario deve conter ate 160 caracteres")]
    public string Content {get;set;} = null!;
    public ENoteCategory Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}