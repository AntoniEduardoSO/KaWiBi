namespace KaWiBi.Core.Models;
public class Department
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public decimal? Lat { get; set; }
    public decimal? Lon { get; set; }

    public ICollection<Asset> Assets { get; set; } =  [];
}