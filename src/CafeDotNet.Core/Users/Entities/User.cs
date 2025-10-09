using CafeDotNet.Core.Base.Entities;
using CafeDotNet.Core.Users.ValueObjects;

namespace CafeDotNet.Core.Users.Entities;

public class User : EntityBase
{
    public string Username { get; private set; }
    public Password Password { get; private set; }

    public RoleType Role { get; private set; }

    protected User() { }

    public User(string username, Password password)
    {
        Username = !string.IsNullOrWhiteSpace(username) ? username : throw new ArgumentException("Username não pode ser vazio.", nameof(username));
        Password = password;
        Role = RoleType.None;

        Activate();
    }

    public void ChangePassword(Password newPassword)
    {
        Password = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
        
        SetUpdated();
    }

    public void RemoveRole() => Role = RoleType.None;
    public void SetAsAdmin() => Role = RoleType.Admin;
    public void SetAsVisitor() => Role = RoleType.Visitor;
}
