using CafeDotNet.Core.Users.Entities;
using System.ComponentModel.DataAnnotations;

namespace CafeDotNet.Core.Users.DTOs;

public class AuthenticationRequest
{
    [Required(ErrorMessage = "Usuário é obrigatório.")]
    [Display(Name = "Usuário")]
    [MaxLength(User.UsernameMaxLength, ErrorMessage = "Usuário deve ter no máximo {1} caracteres.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória.")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string? Password { get; set; }

    [Display(Name = "Lembrar-me")]
    public bool RememberMe { get; set; }
}
