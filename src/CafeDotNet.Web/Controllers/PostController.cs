using CafeDotNet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CafeDotNet.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new PageViewModel
            {
                Header = HeaderViewModel.Create(
                    bannerImagemPath: Url.Content("~/img/post-bg.jpg"),
                    logoTitleImagemPath: Url.Content("~/img/svg/logo-border-black.svg"),
                    title: "Cafe.Net - Post",
                    subTitle: "Reflexões servidas com código e café gourmet"
                )
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
