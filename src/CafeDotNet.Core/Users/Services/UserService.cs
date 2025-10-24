using CafeDotNet.Core.Users.Entities;
using CafeDotNet.Core.Users.Interfaces;
using CafeDotNet.Core.Users.ValueObjects;

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
        var user = await _userRepository.GetUserAsync(username);
       
        //AssertionConcern.AssertArgumentNotNull(nameof(user), user, $"Usuário '{username}' não encontrado no sistema.");

        //if (AssertionConcern.HasErrors)
        //    return null;

        var passwordVO = Password.FromHash(user!.Password.Hash, user!.Password.Salt);

        //AssertionConcern.AssertArgumentTrue(nameof(user), passwordVO.Verify(plainPassword), "Senha incorreta.");

        //if (AssertionConcern.HasErrors)
        //    return null;

        return user;
    }
}
