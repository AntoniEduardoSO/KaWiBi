using System.ComponentModel.DataAnnotations;
using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Requests.Asset;
public class CreateAssetRequest : Request
{
    public EObjectCategory Category { get; set; }

    [Required(ErrorMessage ="Nome do Objeto invalido")]
    [MaxLength(180, ErrorMessage = "O nome do Objeto precisa ter menos que 180 caracteres")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage ="Serial number do Objeto invalido")]
    [MaxLength(255, ErrorMessage = "Serial number do Objeto precisa ter menos que 255 caracteres")]
    public string SerialNumber { get; set; } = null!;

    [Required(ErrorMessage ="Marca do Objeto invalido")]
    [MaxLength(30, ErrorMessage = "A marca do Objeto precisa ter menos que 30 caracteres")]
    public string Stamp { get; set; } = null!;

    [Required(ErrorMessage ="Modelo do Objeto invalido")]
    [MaxLength(180, ErrorMessage = "Modelo do Objeto precisa ter menos que 30 caracteres")]
    public string Pattern { get; set; } = null!;
    
    [RegularExpression(@"^\d{1,3}(\.\d{1,3}){3}$", ErrorMessage = "IpAddress deve estar no formato 'xxx.xxx.xxx.xxx'.")]
    public string IpAddress { get; set; } = string.Empty;
    public long DepartmentId { get; set; }
}