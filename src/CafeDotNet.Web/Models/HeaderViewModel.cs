namespace CafeDotNet.Web.Models;

public sealed class HeaderViewModel
{
    public string BannerImagemPath { get; }
    public string BannerBackgroundClass { get; }
    public string LogoTitleImagemPath { get; }
    public string Title { get; }
    public string SubTitle { get; }

    private HeaderViewModel(string bannerImagemPath, string bannerBackgroundClass, string logoTitleImagemPath, string title, string subTitle)
    {
        BannerImagemPath = bannerImagemPath;
        BannerBackgroundClass = bannerBackgroundClass;
        LogoTitleImagemPath = logoTitleImagemPath;
        Title = title;
        SubTitle = subTitle;
    }

    public static HeaderViewModel Create(string bannerImagemPath, string bannerBackgroundClass, string logoTitleImagemPath, string title, string subTitle)
        => new(bannerImagemPath, bannerBackgroundClass, logoTitleImagemPath, title, subTitle);
}
