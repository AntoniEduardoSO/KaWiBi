using KaWiBi.Core.Enums;

namespace KaWiBi.Core.Responses.Asset;
public class AssetResponse
{
    public long Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public EObjectCategory Category { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string Stamp { get; set; } = string.Empty;
    public string Pattern { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty; // "192.168.1.10"
    public long DepartmentId { get; set; }
}