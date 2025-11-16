using CafeDotNet.Core.Base.Repositories;
using CafeDotNet.Core.Galery.DTOs;
using CafeDotNet.Core.Galery.Entities;

namespace CafeDotNet.Core.Galery.Interfaces;

public interface IImageRepository : IBaseRepository<Image>
{
    Task<ImageUrlDto?> GetImageUrlByIdAsync(int id);
    Task<IEnumerable<ImageListItemDto>> GetActiveImagesAsync();
}
