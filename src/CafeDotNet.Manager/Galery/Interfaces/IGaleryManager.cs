using CafeDotNet.Core.Galery.DTOs;
using CafeDotNet.Core.Gallery.DTOs;

namespace CafeDotNet.Manager.Galery.Interfaces
{
    public interface IGaleryManager
    {
        Task<UploadImageResponse> UploadImageAsync(UploadImageRequest request, Stream fileStream);

        Task<UpdateImageResponse> UpdateImageAsync(UpdateImageRequest request, Stream? newFileStream = null);

        Task<IEnumerable<ImageListItemResponse>> GetActiveImagesAsync();

        Task<GetImagemUrlByIdResponse?> GetImageByIdAsync(long id);
       
        Task<DeleteImageResponse> DeleteImageAsync(long id);
    }
}
