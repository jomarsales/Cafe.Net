using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Core.Users.DTOs;
using CafeDotNet.Manager.Users.Interfaces;
using CafeDotNet.Web.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeDotNet.Web.Controllers;

public class AccountController : BaseController
{
    private readonly IAuthenticationManager _authenticationManager;

    public AccountController(
        IAuthenticationManager authenticationManager, 
        ILogger<BaseController> logger, 
        IHandler<DomainNotification> notifications) : 
        base(logger, notifications)
    {
        _authenticationManager = authenticationManager;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var request = new AuthenticationRequest
        {
            Username = model.Username,
            Password = model.Password
        };

        var result = await _authenticationManager.AuthenticateUserAsyn(request);

        if (HasErrors())
            return View(model);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, result.Username!),
            new(ClaimTypes.Role, result.Role.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = model.RememberMe,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return Redirect(returnUrl ?? Url.Action("Index", "Admin")!);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return View();
    }

    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
