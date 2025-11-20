using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Core.Galery.DTOs;
using CafeDotNet.Core.Galery.Entities;
using CafeDotNet.Core.Galery.Interfaces;
using CafeDotNet.Core.Validation;
using static System.Net.Mime.MediaTypeNames;
using Image = CafeDotNet.Core.Galery.Entities.Image;

namespace CafeDotNet.Core.Galery.Services;

public class ImageService : DomainService, IImageService
{
    private readonly IImageRepository _imagemRepository;

    public ImageService(
        IHandler<DomainNotification> handler,
        IImageRepository imagemRepository
    ) : base(handler)
    {
        _imagemRepository = imagemRepository;
    }

    public async Task<Image?> GetImageByIdAsync(long id)
    {
        return await _imagemRepository.GetImageByIdAsync(id);
    }

    public async Task<IEnumerable<ImageListItemResponse>> GetActiveImagesAsync()
    {
        return await _imagemRepository.GetActiveImagesAsync();
    }

    public async Task<GetImagemUrlByIdResponse?> GetImageUrlByIdAsync(long id)
    {
        return await _imagemRepository.GetImageUrlByIdAsync(id);
    }

    public async Task<Image> AddImageAsync(Image image)
    {
        AddNotification(AssertionConcern.AssertArgumentNotNull(nameof(image), image, "Imagem inválida."));

        if (HasCompliance(image.ValidationResult))
        {
            await _imagemRepository.AddAsync(image);
        }

        return image;
    }

    public async Task<Image> UpdateImageAsync(Image image)
    {
        AddNotification(AssertionConcern.AssertArgumentNotNull(nameof(image), image, "Imagem inválida."));

        if (HasCompliance(image.ValidationResult))
        {
            await _imagemRepository.UpdateAsync(image);
        } 

        return image;
    }

    public async Task<string?> DeleteImageAsync(long id)
    {
        var deletedFilename = string.Empty;

        var image = await _imagemRepository.GetImageByIdAsync(id);

        if (image != null)
        { 
            deletedFilename = image.FileName;

            await _imagemRepository.DeleteAsync(image!);
        }

        return deletedFilename;
    }
}
