using KaWiBi.Api.Data;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Department;
using KaWiBi.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace KaWiBi.Api.Handlers;
public class DepartmentHandler(AppDbContext context) : IDepartmentHandler
{
    public async Task<Response<Department>> CreateAsync(CreateDepartmentRequest request)
    {
        try
        {
            var department = new Department
            {
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                Lat = request.Lat ?? null,
                Lon = request.Lon ?? null,
            };

            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();

            return new Response<Department>(department, 201, "Local criado com sucesso!");
        }
        catch
        {
            return new Response<Department>(null, 500, "Nao foi possivel criar um Local!");
        }
    }

    public async Task<Response<Department>> DeleteAsync(DeleteDepartmentRequest request)
    {
        try
        {
            var department = await context
                .Departments
                .FirstOrDefaultAsync(t => t.Id == request.Id);

            if (department == null)
                return new Response<Department>(null, 404, "Local nao encontrado!");

            context.Departments.Remove(department);
            await context.SaveChangesAsync();

            return new Response<Department>(department, message: "Local excluido com sucesso!");
        }
        catch
        {
            return new Response<Department>(null, 500, "Nao foi possivel excluir o Local");
        }
    }

    public async Task<PagedResponse<List<Department>>> GetAllAsync(GetAllDepartmentRequest request)
    {
        try
        {
            var query = context
                    .Departments
                    .AsNoTracking()
                    .OrderByDescending(x => x.Name);
    
            var departments = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
    
            var count = await query.CountAsync();
    
            return new PagedResponse<List<Department>>(
                departments,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch
        {
            return new PagedResponse<List<Department>>(null, 500, "Nao foi possivel recuperar os locais.");
        }
    }

    public async Task<Response<Department>> GetByIdAsync(GetDepartmentByIdRequest request)
    {
        try
        {
            var department = await context.Departments.SingleOrDefaultAsync(x => x.Id == request.Id);
    
            return department is null 
                ? new Response<Department>(null,404,"Local nao existe!") 
                : new Response<Department>(department);
        }
        catch
        {
            return new Response<Department>(null, 500, "Nao foi possivel recuperar o Local");
        }
    }

    public async Task<Response<Department>> UpdateAsync(UpdateDepartmentRequest request)
    {
        try
        {
            var department = await context.Departments.SingleOrDefaultAsync(x => x.Id == request.Id);
    
            if (department is null)
                return new Response<Department>(null, 404, "Local nao existe!");
    
            department.Name = request.Name;
            department.Location = request.Location;
            department.Description = request.Description;
            department.Lat = request.Lat;
            department.Lon = request.Lon;
    
            context.Departments.Update(department);
            await context.SaveChangesAsync();
    
            return new Response<Department>(department, message: "Local atualizado com sucesso");
        }
        catch
        { 
            return new Response<Department>(null, 500,"Nao foi possivel atualizar o Local");
        }
    }

    public async Task<PagedResponse<List<Asset>>> GetAllAssetByDeparmentAsync(GetAllAssetByDepartmentRequest request)
    {
        try
        {
            var query = context
                    .Assets
                    .AsNoTracking()
                    .Where(x => x.DepartmentId == request.Id)
                    .OrderByDescending(x => x.Name);

            var assets = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Asset>>(
                assets,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch
        {
            return new PagedResponse<List<Asset>>(null, 500, "Nao foi possivel recuperar os Objetos.");
        }
    }
}