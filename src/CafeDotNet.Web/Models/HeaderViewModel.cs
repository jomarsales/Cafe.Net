namespace CafeDotNet.Web.Models;

public sealed class HeaderViewModel
{
    public string BannerImagemPath { get; }
    public string LogoTitleImagemPath { get; }
    public string Title { get; }
    public string SubTitle { get; }

    private HeaderViewModel(string bannerImagemPath, string logoTitleImagemPath, string title, string subTitle)
    {
        BannerImagemPath = bannerImagemPath;
        LogoTitleImagemPath = logoTitleImagemPath;
        Title = title;
        SubTitle = subTitle;
    }

    public static HeaderViewModel Create(string bannerImagemPath, string logoTitleImagemPath, string title, string subTitle)
        => new(bannerImagemPath, logoTitleImagemPath, title, subTitle);
}
