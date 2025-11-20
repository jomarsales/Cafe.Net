using CafeDotNet.Core.Galery.Entities;
using System.ComponentModel.DataAnnotations;

namespace CafeDotNet.Core.Gallery.DTOs;

public class UploadImageRequest
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long Size { get; set; }

    [MaxLength(Image.DescriptionMaxLength, ErrorMessage = "Descrição da imagem deve ter no máximo {1} caracteres.")]
    public string? Description { get; set; }
}
