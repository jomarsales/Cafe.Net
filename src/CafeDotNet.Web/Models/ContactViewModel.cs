using System.ComponentModel.DataAnnotations;

namespace CafeDotNet.Web.Models;

public class ContactViewModel : PageViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(5, ErrorMessage = "Digite um nome com mais caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [Phone(ErrorMessage = "Telefone inválido.")]
    [MinLength(10, ErrorMessage = "Telefone inválido.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "A mensagem é obrigatória.")]
    [MinLength(5, ErrorMessage = "Digite uma mensagem com mais caracteres.")]
    public string Message { get; set; }

    public ContactViewModel()
    {
        Header = HeaderViewModel.Create(
            bannerImagemPath: "../img/contact-bg.jpg",
            bannerBackgroundClass: "bg-logo-light",
            logoTitleImagemPath: "../img/svg/logo-full-black.svg",
            title: "Cafe.Net - Contato",
            subTitle: "Manda uma mensagem, eu preparo o café"
        );
    }
}
