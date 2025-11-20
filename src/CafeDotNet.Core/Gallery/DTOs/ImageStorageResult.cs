namespace CafeDotNet.Core.Gallery.DTOs;

public class ImageStorageResult
{
    public string FileName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? Description { get; set; }
}
