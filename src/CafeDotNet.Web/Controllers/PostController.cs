using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Manager.Articles.Dtos;
using CafeDotNet.Manager.Helpers;
using CafeDotNet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeDotNet.Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly List<ArticleListItemDto> _mockArtigos;

        public PostController(ILogger<PostController> logger, IHandler<DomainNotification> notifications) : base(logger, notifications)
        {
            _mockArtigos = Enumerable.Range(1, 27)
                .Select(i => new ArticleListItemDto(i, $"Artigo de Exemplo #{i}", i % 2 == 0 ? "Um subtítulo opcional" : ""))
                .ToList();
        }

        [HttpGet]
        [Route("Artigos/Pagina/{page:int}")]
        public IActionResult Index(int page = 1)
        {
            var paginatedList = PaginatedList<ArticleListItemDto>.Create(_mockArtigos, page);
            //var paginatedList = PaginatedList<Article>.CreateAsync(_context.Artigos.AsNoTracking(), pagina, itensPorPagina);

            var viewModel = new ArticleListPageViewModel
            {
                Articles = paginatedList
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Post(int id)
        {
            var model = new PageViewModel
            {
                Header = HeaderViewModel.Create(
                    bannerImagemPath: Url.Content("~/img/post-bg.jpg"),
                    "bg-logo-light",
                    logoTitleImagemPath: Url.Content("~/img/svg/logo-border-black.svg"),
                    title: "Cafe.Net - Post",
                    subTitle: "Reflexões servidas com código e café gourmet"
                )
            };

            return View(model);
        }
    }
}
