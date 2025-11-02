using System.ComponentModel.DataAnnotations;

namespace CafeDotNet.Core.Users.DTOs;

public class ChangePasswordRequest
{
    public long Id { get; set; }

    [Required(ErrorMessage = "A senha atual é obrigatória.")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "A nova senha é obrigatória.")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "A nova senha deve ter pelo menos 6 caracteres.")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "A confirmação da senha não confere.")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}
