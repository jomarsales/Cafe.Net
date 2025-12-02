using CafeDotNet.Core.Base.DTOs;

namespace CafeDotNet.Core.Gallery.DTOs;

public class UploadImageResponse : BaseDTOResponse
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? Description { get; set; }
}
