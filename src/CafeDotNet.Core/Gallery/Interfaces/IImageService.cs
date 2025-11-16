using CafeDotNet.Core.Base.Repositories;
using CafeDotNet.Core.Galery.DTOs;

namespace CafeDotNet.Core.Galery.Interfaces;

public interface IImageService
{
    Task<IEnumerable<ImageListItemDto>> GetActiveImagesAsync();

    Task<ImageUrlDto?> GetImageUrlByIdAsync(int id);

    Task<Entities.Image> AddImageAsync(Entities.Image image);

    Task<Entities.Image> UpdateImageAsync(Entities.Image image);
}
