using CafeDotNet.Core.Base.ValueObjects;
using CafeDotNet.Core.Validation;

namespace CafeDotNet.Core.Users.DTOs;

public class AuthenticationRequest : ValueObjectBase
{
    public string? Username { get; set; }
    public string? Password { get; set; }

    protected override void Validate()
    {
        ValidationResult.Add(AssertionConcern.AssertArgumentNotNull(nameof(Username), Username, "Usuário deve ser informado."));
        ValidationResult.Add(AssertionConcern.AssertArgumentNotNull(nameof(Password), Password, "Senha deve ser informado."));
    }
}
