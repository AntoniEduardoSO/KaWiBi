namespace KaWiBi.Core.Responses.Asset;
public class PagedAssetResponse
{
    public List<AssetResponse> Data { get; set; } = [];
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}