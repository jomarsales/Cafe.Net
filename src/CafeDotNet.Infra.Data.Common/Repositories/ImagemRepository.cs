using CafeDotNet.Core.Galery.DTOs;
using CafeDotNet.Core.Galery.Entities;
using CafeDotNet.Core.Galery.Interfaces;
using CafeDotNet.Infra.Data.Common.Context;
using Microsoft.EntityFrameworkCore;

namespace CafeDotNet.Infra.Data.Common.Repositories;

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    private readonly CafeDbContext _cafeDbContext;

    public ImageRepository(CafeDbContext cafeDbContext) : base(cafeDbContext)
    {
        _cafeDbContext = cafeDbContext;
    }

    public async Task<Image?> GetImageByIdAsync(long id)
    {
        return await _cafeDbContext.Images.FindAsync(id);
    }

    public async Task<GetImagemUrlByIdResponse?> GetImageUrlByIdAsync(long id)
    {
        return await _cafeDbContext.Images
            .Where(img => img.Id == id && img.IsActive)
            .Select(img => new GetImagemUrlByIdResponse
            {
                Id = img.Id,
                Url = "../../img/galery/" + img.FileName
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ImageListItemResponse>> GetActiveImagesAsync()
    {
        return await _cafeDbContext.Images
            .Where(img => img.IsActive)
            .Select(img => new ImageListItemResponse
            {
                Id = img.Id,
                FileName = img.FileName,
                ContentType = img.ContentType,
                Url = "../../img/galery/" + img.FileName
            })
            .ToListAsync();
    }
}
