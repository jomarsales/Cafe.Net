using System.ComponentModel;

namespace CafeDotNet.Core.Users.ValueObjects;

public enum RoleType
{
    [Description("Nenhum")]
    None,

    [Description("Administrador")]
    Admin,

    [Description("Visitante")]
    Visitor
}
