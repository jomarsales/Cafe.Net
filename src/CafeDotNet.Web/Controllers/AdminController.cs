using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeDotNet.Web.Controllers;

[Authorize]
public class AdminController : BaseController
{
    public AdminController(ILogger<BaseController> logger, IHandler<DomainNotification> notifications) : base(logger, notifications)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
