using AutoMapper;
using CafeDotNet.Core.Galery.DTOs;
using CafeDotNet.Core.Galery.Entities;
using CafeDotNet.Core.Gallery.DTOs;

namespace CafeDotNet.Infra.Bootstraper.ProfileMappers;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Image, UploadImageResponse>()
            .ForMember(dest => dest.Url, img => img.MapFrom(org => org.ToString()));

        CreateMap<Image, GetImagemUrlByIdResponse>();
        CreateMap<Image, GetImagemUrlByIdResponse>();
    }
}
