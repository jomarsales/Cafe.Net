using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Core.Galery.DTOs;
using CafeDotNet.Core.Galery.Interfaces;
using CafeDotNet.Core.Validation;

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

    public async Task<IEnumerable<ImageListItemDto>> GetActiveImagesAsync()
    {
        return await _imagemRepository.GetActiveImagesAsync();
    }

    public async Task<ImageUrlDto?> GetImageUrlByIdAsync(int id)
    {
        return await _imagemRepository.GetImageUrlByIdAsync(id);
    }

    public async Task<Entities.Image> AddImageAsync(Entities.Image image)
    {
        AddNotification(AssertionConcern.AssertArgumentNotNull(nameof(image), image, "Imagem inválida."));

        if (HasCompliance(image.ValidationResult))
        {
            await _imagemRepository.AddAsync(image);
        }

        return image;
    }

    public async Task<Entities.Image> UpdateImageAsync(Entities.Image image)
    {
        AddNotification(AssertionConcern.AssertArgumentNotNull(nameof(image), image, "Imagem inválida."));

        if (HasCompliance(image.ValidationResult))
        {
            await _imagemRepository.UpdateAsync(image);
        } 

        return image;
    }
}
