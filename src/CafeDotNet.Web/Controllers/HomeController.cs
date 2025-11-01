using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Infra.Mail.Interfaces;
using CafeDotNet.Web.Enums;
using CafeDotNet.Web.Helpers;
using CafeDotNet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeDotNet.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IEmailService _emailService;

        public HomeController(
            ILogger<HomeController> logger,
            IEmailService emailService,
            IHandler<DomainNotification> notifications) : 
            base(logger, notifications)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new PageViewModel
            {
                Header = HeaderViewModel.Create(
                    bannerImagemPath: Url.Content("~/img/home-bg.jpg"),
                    "bg-logo-light",
                    logoTitleImagemPath: Url.Content("~/img/svg/logo-yellow-black.svg"),
                    title: "Cafe.Net",
                    subTitle: "Inspirando desenvolvedores .NET a codificar melhor, criar mais e inovar sempre"
                )
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult About()
        {
            var model = new PageViewModel
            {
                Header = HeaderViewModel.Create(
                    bannerImagemPath: Url.Content("~/img/about-bg.jpg"),
                    string.Empty,
                    logoTitleImagemPath: Url.Content("~/img/svg/logo-yellow-white.svg"),
                    title: "Cafe.Net - Sobre mim",
                    subTitle: "Entre uma xícara de café e outra, escrevo código que inspira"
                )
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var body = _emailService.CreateEmailBody(model.Name, model.Email, model.Message);

                await _emailService.SendEmailAsync("cafedotnetcontact@gmail.com", $"Nova mensagem de {model.Email}", body);

                this.SetAlert("Mensagem enviada com sucesso! Entraremos em contato em breve.", AlertType.Success);
            }
            catch (Exception ex)
            {
                this.SetAlert($"Ocorreu um erro: {ex.Message}", AlertType.Danger);
            }

            return RedirectToAction(nameof(Contact));
        }
    }
}
