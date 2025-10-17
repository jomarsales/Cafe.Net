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
        AssertionConcern.AssertArgumentNotEmpty(nameof(Username), username, "Usuário não pode ser vazio.");
        AssertionConcern.AssertArgumentLength(nameof(Username), username, UsernameMaxLength, $"Usuário precisa conter até {User.UsernameMaxLength} caracteres.");

        AssertionConcern.AssertArgumentNotNull(nameof(password), password, "Semha não pode ser null.");

        if (AssertionConcern.HasErrors)
            return;

        Username = username;
        Password = password;
        Role = RoleType.None;

        Activate();
    }

    public void ChangePassword(Password newPassword)
    {
        AssertionConcern.AssertArgumentNotNull(nameof(Password), newPassword, "Semha não pode ser null.");

        if (AssertionConcern.HasErrors)
            return;

        SetUpdated();
    }

    public void RemoveRole() => Role = RoleType.None;
    public void SetAsAdmin() => Role = RoleType.Admin;
    public void SetAsVisitor() => Role = RoleType.Visitor;
}
