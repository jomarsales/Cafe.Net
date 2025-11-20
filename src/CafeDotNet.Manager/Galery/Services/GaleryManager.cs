using AutoMapper;
using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Core.Galery.DTOs;
using CafeDotNet.Core.Galery.Interfaces;
using CafeDotNet.Core.Gallery.DTOs;
using CafeDotNet.Core.Gallery.Interfaces;
using CafeDotNet.Core.Validation;
using CafeDotNet.Infra.Data.Common.Interfaces;
using CafeDotNet.Manager.Application;
using CafeDotNet.Manager.Galery.Interfaces;
using Image = CafeDotNet.Core.Galery.Entities.Image;

namespace CafeDotNet.Manager.Galery.Services;

public class GaleryManager : ApplicationManager, IGaleryManager
{
    private readonly IImageService _imageService;
    private readonly IImageStorage _imageStorage;
    private readonly IMapper _mapper;

    public GaleryManager(
        IUnitOfWork unitOfWork,
        IHandler<DomainNotification> notifications,
        IImageService imageService,
        IImageStorage imageStorage,
        IMapper mapper)
        : base(unitOfWork, notifications)
    {
        _imageService = imageService;
        _imageStorage = imageStorage;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ImageListItemResponse>> GetActiveImagesAsync()
    {
        var images = await _imageService.GetActiveImagesAsync();
       
        return _mapper.Map<IEnumerable<ImageListItemResponse>>(images);
    }

    public async Task<GetImagemUrlByIdResponse?> GetImageByIdAsync(long id)
    {
        var image = await _imageService.GetImageUrlByIdAsync(id);
      
        return _mapper.Map<GetImagemUrlByIdResponse?>(image);
    }

    public async Task<UploadImageResponse> UploadImageAsync(UploadImageRequest request, Stream fileStream)
    {
        if (request == null)
        {
            AddNotification(new ValidationError(nameof(Image), "Requisição inválida para upload de imagem."));
            
            return new UploadImageResponse();
        }

        if (fileStream == null || !fileStream.CanRead)
        {
            AddNotification(new ValidationError(nameof(Image), "O stream da imagem é inválido."));

            return new UploadImageResponse();
        }

        var storageResult = await _imageStorage.SaveAsync(request.FileName, fileStream, request.ContentType);
        
        if (storageResult is null)
        {
            AddNotification(new ValidationError(nameof(Image), "Falha ao salvar a imagem no storage."));

            return new UploadImageResponse();
        }

        var image = await _imageService.AddImageAsync(new Image(storageResult.FileName, request.ContentType, request.Size, storageResult.Description));
        
        var commitOk = await CommitAsync();

        var dto = _mapper.Map<UploadImageResponse>(image);
        
        dto.Success = commitOk;

        return dto;
    }

    public async Task<UpdateImageResponse> UpdateImageAsync(UpdateImageRequest request, Stream? newFileStream = null)
    {
        var image = await _imageService.GetImageByIdAsync(request.Id);

        if (image == null)
        {
            AddNotification(new ValidationError(nameof(Image), $"Imagem com ID {request.Id} não encontrada."));

            return new UpdateImageResponse { Success = false };
        }


        if (newFileStream != null)
        {
            var oldFileName = Path.GetFileName(image.ToString());
            
            await _imageStorage.DeleteAsync(oldFileName);

            var storageResult = await _imageStorage.SaveAsync(request.FileName, newFileStream, request.ContentType);

            if (storageResult == null)
            {
                AddNotification(new ValidationError(nameof(Image), "Falha ao salvar nova imagem."));

                return new UpdateImageResponse { Success = false };
            }
        }

        image = await _imageService.UpdateImageAsync(image);

        var commitOk = await CommitAsync();

        var dto = _mapper.Map<UpdateImageResponse>(image);

        dto.Success = commitOk;

        return dto;
    }

    public async Task<DeleteImageResponse> DeleteImageAsync(long id)
    {
        var deletedFileName = await _imageService.DeleteImageAsync(id);
        
        var commitOk = await CommitAsync();

        var response = new DeleteImageResponse { Success = commitOk };

        if(response.Success == false || deletedFileName == null)
        {
            AddNotification(new ValidationError(nameof(Image), "Falha ao deletar imagem do banco de dados."));

            return response;
        }

        var fileDeleted = await _imageStorage.DeleteAsync(deletedFileName);

        if(!fileDeleted)
        {
            AddNotification(new ValidationError(nameof(Image), "Falha ao deletar imagem do storage."));
            
            response.Success = false;
        }

        return response;
    }
}
