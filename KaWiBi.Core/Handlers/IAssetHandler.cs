using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Asset;
using KaWiBi.Core.Responses;
using KaWiBi.Core.Responses.Asset;

namespace KaWiBi.Core.Handlers;
public interface IAssetHandler
{
    Task<Response<AssetResponse>> CreateAsync(CreateAssetRequest request);
    Task<Response<AssetResponse>> UpdateAsync(UpdateAssetRequest request);
    Task<Response<Asset>> DeleteAsync(DeleteAssetRequest request);
    Task<Response<AssetResponse>> GetByIdAsync(GetAssetByIdRequest request);
    Task<PagedResponse<List<AssetResponse>>> GetAllAsync(GetAllAssetRequest request);
}