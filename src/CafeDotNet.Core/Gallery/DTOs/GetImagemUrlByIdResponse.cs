namespace CafeDotNet.Core.Galery.DTOs;

public class GetImagemUrlByIdResponse
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? Description { get; set; }
}
