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
            _logger.LogInformation("Custom Information: Index page visited at {Time}", DateTime.UtcNow);
            return View();
        }

        public IActionResult Privacy()
        {
            throw new InvalidOperationException("Erro ao tentar acessar a página de privacidade.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
