namespace CafeDotNet.Core.Gallery.DTOs;

public class UpdateImageRequest
{
    public long Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long Size { get; set; }
    public string? Description { get; set; }
    public string Url { get; set; }
}
