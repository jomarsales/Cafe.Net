using CafeDotNet.Core.Base.Entities;
using CafeDotNet.Core.Validation;

namespace CafeDotNet.Core.Galery.Entities;

public class Image : EntityBase
{
    public const string PathFolder = "img/galery/";
    public const int FileNameMaxLength = 255;
    public const int OriginalNameMaxLength = 255;
    public const int ContentTypeMaxLength = 50;
    public const int DescriptionMaxLength = 500;

    public string FileName { get; private set; }
    public string ContentType { get; private set; }
    public long Size { get; private set; }
    public string? Description { get; private set; }

    protected Image() { }

    public Image(
        string fileName,
        string contentType,
        long size,
        string? description = null)
    {
        FileName = fileName;
        ContentType = contentType;
        Size = size;
        Description = description;

        Activate();
        Validate();
    }

    public void ChangeDescription(string? newDescription)
    {
        Description = newDescription;
        SetUpdated();
        Validate();
    }

    protected override void Validate()
    {
        ValidationResult.Add(AssertionConcern.AssertArgumentNotEmpty(nameof(FileName), FileName, "Nome do arquivo não pode ser vazio."));
        ValidationResult.Add(AssertionConcern.AssertArgumentLength(nameof(FileName), FileName, FileNameMaxLength, $"Nome do arquivo deve ter até {FileNameMaxLength} caracteres."));

        ValidationResult.Add(AssertionConcern.AssertArgumentNotEmpty(nameof(ContentType), ContentType, "Content-Type deve ser informado."));
        ValidationResult.Add(AssertionConcern.AssertArgumentLength(nameof(ContentType), ContentType, ContentTypeMaxLength, $"Content-Type deve ter até {ContentTypeMaxLength} caracteres."));

        ValidationResult.Add(AssertionConcern.AssertArgumentTrue(nameof(Size), Size > 0, "Tamanho do arquivo inválido."));

        if (!string.IsNullOrWhiteSpace(Description))
        {
            ValidationResult.Add(AssertionConcern.AssertArgumentLength(nameof(Description), Description, DescriptionMaxLength, $"Descrição deve ter até {DescriptionMaxLength} caracteres."));
        }
    }

    public override string ToString()
    {
        return Path.Combine(PathFolder, FileName);
    }
}
