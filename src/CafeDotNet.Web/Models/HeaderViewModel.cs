namespace CafeDotNet.Web.Models;

public sealed class HeaderViewModel
{
    public string BannerImagePath { get; }
    public string BannerBackgroundClass { get; }
    public string LogoTitleImagePath { get; }
    public string Title { get; }
    public string SubTitle { get; }

    private HeaderViewModel(string bannerImagePath, string bannerBackgroundClass, string logoTitleImagePath, string title, string subTitle)
    {
        BannerImagePath = bannerImagePath;
        BannerBackgroundClass = bannerBackgroundClass;
        LogoTitleImagePath = logoTitleImagePath;
        Title = title;
        SubTitle = subTitle;
    }

    public static HeaderViewModel Create(string bannerImagePath, string bannerBackgroundClass, string logoTitleImagePath, string title, string subTitle)
        => new(bannerImagePath, bannerBackgroundClass, logoTitleImagePath, title, subTitle);
}
