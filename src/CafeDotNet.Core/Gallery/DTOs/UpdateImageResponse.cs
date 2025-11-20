using CafeDotNet.Core.Base.DTOs;

namespace CafeDotNet.Core.Galery.DTOs;

public class UpdateImageResponse : BaseDTOResponse
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? Description { get; set; }
}