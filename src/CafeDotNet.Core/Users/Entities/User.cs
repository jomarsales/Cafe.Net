using CafeDotNet.Core.Base.Entities;
using CafeDotNet.Core.Users.ValueObjects;
using CafeDotNet.Core.Validation;

namespace CafeDotNet.Core.Users.Entities;

public class User : EntityBase
{
    public const int UsernameMaxLength = 100;

    public string Username { get; private set; }
    public Password Password { get; private set; }

    public RoleType Role { get; private set; }

    protected User() { }

    public User(string username, Password password)
    {
        Username = username;
        Password = password;
        Role = RoleType.None;

        Activate();
        Validate();
    }

    public void ChangePassword(Password newPassword)
    {
        Password = newPassword;

        SetUpdated();
        Validate();
    }

    public void RemoveRole() => Role = RoleType.None;
    public void SetAsAdmin() => Role = RoleType.Admin;
    public void SetAsVisitor() => Role = RoleType.Visitor;

    protected override void Validate()
    {
        ValidationResult.Add(AssertionConcern.AssertArgumentNotEmpty(nameof(Username), Username, "Usuário não pode ser vazio."));
        ValidationResult.Add(AssertionConcern.AssertArgumentLength(nameof(Username), Username, UsernameMaxLength, $"Usuário precisa conter até {UsernameMaxLength} caracteres."));

        ValidationResult.Add(AssertionConcern.AssertArgumentNotNull(nameof(Password), Password, "Senha deve ser informada."));
        if (Password != null)
        {
            if (!Password.ValidationResult.IsValid)
            {
                foreach (var error in Password.ValidationResult.Errors)
                {
                    ValidationResult.Add(error);
                }
            }
        }
    }
}
