using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Department;
using KaWiBi.Core.Responses;

namespace KaWiBi.Core.Handlers;
public interface IDepartmentHandler
{
    Task<Response<Department>> CreateAsync(CreateDepartmentRequest request);
    Task<Response<Department>> UpdateAsync(UpdateDepartmentRequest request);
    Task<Response<Department>> DeleteAsync(DeleteDepartmentRequest request);
    Task<Response<Department>> GetByIdAsync(GetDepartmentByIdRequest request);
    Task<PagedResponse<List<Department>>> GetAllAsync(GetAllDepartmentRequest request);
    Task<PagedResponse<List<Asset>>> GetAllAssetByDeparmentAsync(GetAllAssetByDepartmentRequest request);
}