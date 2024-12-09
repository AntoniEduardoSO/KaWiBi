using System.ComponentModel.DataAnnotations;

namespace KaWiBi.Core.Requests.Department;
public class UpdateDepartmentRequest : Request
{
    public long Id { get; set; }
    [Required(ErrorMessage ="Nome do local invalido")]
    [MaxLength(180, ErrorMessage = "O nome do Local precisa ter menos que 180 caracteres")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage ="Descricao do local invalido")]
    [MaxLength(255, ErrorMessage = "A descricao do Local precisa ter menos que 255 caracteres")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage ="Localizacao do local invalido")]
    [MaxLength(255, ErrorMessage = "A localizacao do Local precisa ter menos que 255 caracteres")]
    public string Location { get; set; } = null!;

    public decimal? Lat { get; set; }

    public decimal? Lon { get; set; }
}