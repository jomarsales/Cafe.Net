using CafeDotNet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CafeDotNet.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger<BaseController> Logger;

        public BaseController(ILogger<BaseController> logger)
        {
            Logger = logger;
        }
              
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
