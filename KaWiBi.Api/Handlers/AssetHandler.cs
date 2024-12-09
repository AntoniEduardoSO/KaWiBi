using KaWiBi.Api.Data;
using KaWiBi.Core.Handlers;
using KaWiBi.Core.Models;
using KaWiBi.Core.Requests.Asset;
using KaWiBi.Core.Responses;
using KaWiBi.Core.Responses.Asset;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KaWiBi.Api.Handlers;
public class AssetHandler(AppDbContext context) : IAssetHandler
{
    private byte[] ConvertIpStringToBytes(string ipAddressString)
    {
        if (IPAddress.TryParse(ipAddressString, out var ipAddress))
        {
            return ipAddress.GetAddressBytes();
        }
        else
        {
            throw new ArgumentException("Endereço IP inválido.");
        }
    }

    private string ConvertIpBytesToString(byte[] ipAddressBytes)
    {
        var ipAddress = new IPAddress(ipAddressBytes);
        return ipAddress.ToString();
    }
    public async Task<Response<AssetResponse>> CreateAsync(CreateAssetRequest request)
    {
        try
        {
            var department = await context
                .Departments
                .FirstOrDefaultAsync(x => x.Id == request.DepartmentId);

            if (department == null)
                return new Response<AssetResponse>(null, 404, "Departamento atual nao existe!");

            byte[] ipAddressBytes;
            try
            {
                ipAddressBytes = ConvertIpStringToBytes(request.IpAddress);
            }
            catch (ArgumentException ex)
            {
                return new Response<AssetResponse>(null, 400, ex.Message);
            }


            var asset = new Asset
            {
                Category = request.Category,
                Name = request.Name,
                SerialNumber = request.SerialNumber,
                Stamp = request.Stamp,
                Pattern = request.Pattern,
                IpAddress = ipAddressBytes,
                DepartmentId = request.DepartmentId
            };

            await context.Assets.AddAsync(asset);
            await context.SaveChangesAsync();

            var assetResponse = new AssetResponse
            {
                Id = asset.Id,
                Category = asset.Category,
                Name = asset.Name,
                SerialNumber = asset.SerialNumber,
                Stamp = asset.Stamp,
                Pattern = asset.Pattern,
                IpAddress = asset.IpAddress != null ? ConvertIpBytesToString(asset.IpAddress) : string.Empty,
                DepartmentId = asset.DepartmentId
            };

            return new Response<AssetResponse>(assetResponse, 201, "Objeto criado com sucesso!");
        }
        catch
        {
            return new Response<AssetResponse>(null, 500, "Nao foi possivel criar um Obejto!");
        }
    }

    public async Task<Response<Asset>> DeleteAsync(DeleteAssetRequest request)
    {
        try
        {
            var asset = await context.Assets.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (asset is null)
                return new Response<Asset>(null, 404, "Objeto nao existe!");

            context.Assets.Remove(asset);
            await context.SaveChangesAsync();

            return new Response<Asset>(asset, message: "Objeto excluido com sucesso!");
        }
        catch
        {
            return new Response<Asset>(null, 500, "Nao foi possivel exlcuir o Objeto");
        }
    }

    public async Task<PagedResponse<List<AssetResponse>>> GetAllAsync(GetAllAssetRequest request)
    {
        try
        {
            var query = context
                    .Assets
                    .AsNoTracking()
                    .OrderByDescending(x => x.Name);

            var assets = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            var assetResponses = assets.Select(asset => new AssetResponse
            {
                Id = asset.Id,
                Category = asset.Category,
                Name = asset.Name,
                SerialNumber = asset.SerialNumber,
                Stamp = asset.Stamp,
                Pattern = asset.Pattern,
                IpAddress = asset.IpAddress != null ? ConvertIpBytesToString(asset.IpAddress) : string.Empty,
                DepartmentId = asset.DepartmentId
            }).ToList();

            return new PagedResponse<List<AssetResponse>>(
                assetResponses,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<AssetResponse>>(null, 500, "Nao foi possivel consultar os objetos");
        }
    }


    public async Task<Response<AssetResponse>> GetByIdAsync(GetAssetByIdRequest request)
    {
        try
        {
            var asset = await context.Assets.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (asset is null)
                return new Response<AssetResponse>(null, 404, "Objeto nao existe!");

            var assetResponse = new AssetResponse
            {
                Id = asset.Id,
                Category = asset.Category,
                Name = asset.Name,
                SerialNumber = asset.SerialNumber,
                Stamp = asset.Stamp,
                Pattern = asset.Pattern,
                IpAddress = asset.IpAddress != null ? ConvertIpBytesToString(asset.IpAddress) : string.Empty,
                DepartmentId = asset.DepartmentId
            };


            return new Response<AssetResponse>(assetResponse);

        }
        catch
        {
            return new Response<AssetResponse>(null, 500, "Nao foi possivel recuperar o Objeto");
        }
    }

    public async Task<Response<AssetResponse>> UpdateAsync(UpdateAssetRequest request)
    {
        try
        {
            var asset = await context.Assets.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (asset is null)
                return new Response<AssetResponse>(null, 404, "Objeto nao existe!");

            byte[] ipAddressBytes;
            try
            {
                ipAddressBytes = ConvertIpStringToBytes(request.IpAddress);
            }
            catch (ArgumentException ex)
            {
                return new Response<AssetResponse>(null, 400, ex.Message);
            }

            asset.Category = request.Category;
            asset.Name = request.Name;
            asset.SerialNumber = request.SerialNumber;
            asset.Stamp = request.Stamp;
            asset.Pattern = request.Pattern;
            asset.IpAddress = ipAddressBytes;
            asset.DepartmentId = request.DepartmentId;


            context.Assets.Update(asset);
            await context.SaveChangesAsync();

            var assetResponse = new AssetResponse
            {
                Id = asset.Id,
                Category = asset.Category,
                Name = asset.Name,
                SerialNumber = asset.SerialNumber,
                Stamp = asset.Stamp,
                Pattern = asset.Pattern,
                IpAddress = asset.IpAddress != null ? ConvertIpBytesToString(asset.IpAddress) : string.Empty,
                DepartmentId = asset.DepartmentId
            };


            return new Response<AssetResponse>(assetResponse, message: "Objeto atualizado com sucesso");
        }
        catch
        {
            return new Response<AssetResponse>(null, 500, "Nao foi possivel atualizar o Objeto");
        }
    }

}