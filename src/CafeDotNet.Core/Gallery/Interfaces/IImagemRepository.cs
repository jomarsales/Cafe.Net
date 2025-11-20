using CafeDotNet.Core.Base.Repositories;
using CafeDotNet.Core.Galery.DTOs;
using CafeDotNet.Core.Galery.Entities;

namespace CafeDotNet.Core.Galery.Interfaces;

public interface IImageRepository : IBaseRepository<Image>
{
    Task<Image?> GetImageByIdAsync(long id);
    Task<GetImagemUrlByIdResponse?> GetImageUrlByIdAsync(long id);
    Task<IEnumerable<ImageListItemResponse>> GetActiveImagesAsync();
}
