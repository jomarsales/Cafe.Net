using CafeDotNet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CafeDotNet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new PageViewModel
            {
                Header = HeaderViewModel.Create(
                    bannerImagemPath: Url.Content("~/img/home-bg.jpg"),
                    logoTitleImagemPath: Url.Content("~/img/svg/logo-full-black.svg"),
                    title: "Cafe.Net",
                    subTitle: "Inspirando desenvolvedores .NET a codificar melhor, criar mais e inovar sempre"
                )
            };

            return View(model);
        }

        public IActionResult About()
        {
            var model = new PageViewModel
            {
                Header = HeaderViewModel.Create(
                    bannerImagemPath: Url.Content("~/img/about-bg.jpg"),
                    logoTitleImagemPath: Url.Content("~/img/svg/logo-full-white.svg"),
                    title: "Cafe.Net - Sobre mim",
                    subTitle: "Entre uma xícara de café e outra, escrevo código que inspira"
                )
            };

            return View(model);
        }

        public IActionResult Contact()
        {
            var model = new PageViewModel
            {
                Header = HeaderViewModel.Create(
                    bannerImagemPath: Url.Content("~/img/contact-bg.jpg"),
                    logoTitleImagemPath: Url.Content("~/img/svg/logo-yellow-black.svg"),
                    title: "Cafe.Net - Contato",
                    subTitle: "Manda uma mensagem, eu preparo o café"
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
