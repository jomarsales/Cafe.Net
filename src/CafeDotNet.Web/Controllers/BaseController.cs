using CafeDotNet.Core.Base.ValueObjects;
using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Web.Enums;
using CafeDotNet.Web.Helpers;
using CafeDotNet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CafeDotNet.Web.Controllers;

public abstract class BaseController : Controller
{
    protected readonly ILogger<BaseController> Logger;
    protected readonly IHandler<DomainNotification> Notifications;

    public BaseController(ILogger<BaseController> logger, IHandler<DomainNotification> notifications)
    {
        Logger = logger;
        Notifications = notifications;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    protected bool HasErrors()
    {
        if (Notifications.HasNotifications(TypeNotification.Error))
        {
            this.SetAlert(string.Join("</ br>", Notifications.GetValues().Select(x => x.Value)), AlertType.Danger);

            return true;
        }

        return false;
    }
}
