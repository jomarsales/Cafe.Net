using CafeDotNet.Core.Users.DTOs;
using CafeDotNet.Core.Validation;
using CafeDotNet.Manager.Users.Interfaces;
using CafeDotNet.Web.Enums;
using CafeDotNet.Web.Helpers;
using CafeDotNet.Web.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeDotNet.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(IAuthenticationManager authenticationManager)
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

            //if (AssertionConcern.HasErrors)
            //{
            //    this.SetAlert(string.Join("</ br>", AssertionConcern.Errors), AlertType.Danger);

            //    return View(model);
            //}

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
}
