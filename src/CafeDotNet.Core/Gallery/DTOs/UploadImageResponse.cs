namespace CafeDotNet.Core.Gallery.DTOs;

public class UploadImageResponse
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool Success { get; set; } = false;
}
