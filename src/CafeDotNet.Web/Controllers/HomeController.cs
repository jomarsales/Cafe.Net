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
            ViewData["HeaderBackground"] = Url.Content("~/img/home-bg-code.jpg");

            return View();
        }

        public IActionResult About()
        {
            ViewData["HeaderBackground"] = Url.Content("~/img/about-bg.jpg");

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["HeaderBackground"] = Url.Content("~/img/contact-bg.jpg");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
