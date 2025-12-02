using CafeDotNet.Core.Gallery.DTOs;
using CafeDotNet.Core.Gallery.Interfaces;

namespace CafeDotNet.Infra.Data.Common.Services;

/// <summary>
/// Implementação de IImageStorage que salva imagens no disco local (wwwroot/img/galery/).
/// </summary>
public class DiskImageStorage : IImageStorage
{
    private readonly string _baseFolderPath;
    private readonly string _publicBaseUrl;

    public DiskImageStorage()
    {
        var projectRoot = Directory.GetCurrentDirectory();
        _baseFolderPath = Path.Combine(projectRoot, "wwwroot\\img\\galery");
        _publicBaseUrl = "/img/galery/";

        if (!Directory.Exists(_baseFolderPath))
            Directory.CreateDirectory(_baseFolderPath);
    }

    public async Task<ImageStorageResult?> SaveAsync(string fileName, Stream content, string contentType)
    {
        try
        {
            var extension = Path.GetExtension(fileName);
            var uniqueName = $"{Guid.NewGuid():N}{extension}";
            var fullPath = Path.Combine(_baseFolderPath, uniqueName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await content.CopyToAsync(fileStream);
            }

            var result = new ImageStorageResult
            {
                FileName = uniqueName,
                Url = $"{_publicBaseUrl}{uniqueName}".Replace("\\", "/")
            };

            return result;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteAsync(string fileName)
    {
        try
        {
            var fullPath = Path.Combine(_baseFolderPath, fileName);

            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
