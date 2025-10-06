using CafeDotNet.Manager.Articles.Dtos;
using CafeDotNet.Manager.Helpers;

namespace CafeDotNet.Web.Models;

public class ArticleListPageViewModel
{
    public HeaderViewModel Header { get; set; }
    public PaginatedList<ArticleListItemDto> Articles { get; set; } = default!;

    public ArticleListPageViewModel()
    {
        Header = HeaderViewModel.Create(
            bannerImagemPath: "../../img/post-bg.jpg",
            "bg-logo-light",
            logoTitleImagemPath: "../../img/svg/logo-border-black.svg",
            title: "Cafe.Net - Post",
            subTitle: "Reflexões servidas com código e café gourmet"
        );
    }
}
