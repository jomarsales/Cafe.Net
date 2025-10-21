using CafeDotNet.Core.Users.Entities;
using CafeDotNet.Core.Users.Interfaces;
using CafeDotNet.Core.Users.ValueObjects;
using CafeDotNet.Core.Validation;

namespace CafeDotNet.Core.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserAsync(string username, string plainPassword)
    {
        AssertionConcern.Clear();

        AssertionConcern.AssertArgumentNotNull(nameof(username), username, $"O usuário '{username}' é obrigatório no processo de Login.");
        AssertionConcern.AssertArgumentLength(nameof(username), username, User.UsernameMaxLength, $"O usuário '{username}' deve conter no máximo {User.UsernameMaxLength} caracteres.");

        AssertionConcern.AssertArgumentNotNull(nameof(username), username, "A senha é obrigatória no processo de Login.");

        if (AssertionConcern.HasErrors)
            return null;

        var user = await _userRepository.GetUserAsync(username);
       
        AssertionConcern.AssertArgumentNotNull(nameof(user), user, $"Usuário '{username}' não encontrado no sistema.");

        if (AssertionConcern.HasErrors)
            return null;

        var passwordVO = Password.FromHash(user!.Password.Hash, user!.Password.Salt);

        AssertionConcern.AssertArgumentTrue(nameof(user), passwordVO.Verify(plainPassword), "Senha incorreta.");

        if (AssertionConcern.HasErrors)
            return null;

        return user;
    }
}
