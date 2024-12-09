using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Models;
public class Asset
{
    public long Id { get; set; }
    public EObjectCategory Category { get; set; }
    public string Name { get; set; } = null!;
    public string Stamp { get; set; } = null!;
    public string Pattern { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
    public byte[]? IpAddress { get; set; }
    public long DepartmentId { get; set; }
    [NotMapped]
    [JsonIgnore]
    public Department Department { get; set; } = null!;
}