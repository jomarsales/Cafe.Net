using CafeDotNet.Core.Galery.DTOs;
using CafeDotNet.Core.Galery.Entities;

namespace CafeDotNet.Core.Galery.Interfaces;

public interface IImageService
{
    Task<Image?> GetImageByIdAsync(long id);

    Task<IEnumerable<ImageListItemResponse>> GetActiveImagesAsync();

    Task<GetImagemUrlByIdResponse?> GetImageUrlByIdAsync(long id);

    Task<Image> AddImageAsync(Image image);

    Task<Image> UpdateImageAsync(Image image);

    Task<string?> DeleteImageAsync(long id);
}
