namespace CafeDotNet.Infra.Data.Common.Configurations;

public class ImageStorageOptions
{
    public string BaseFolderPath { get; set; } = "wwwroot/img/galery";
    public string PublicBaseUrl { get; set; } = "/img/galery/";
}
