using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Core.Users.DTOs;
using CafeDotNet.Core.Users.Entities;
using CafeDotNet.Core.Users.Interfaces;
using CafeDotNet.Core.Users.ValueObjects;
using CafeDotNet.Core.Validation;

namespace CafeDotNet.Core.Users.Services;

public class UserService : DomainService, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IHandler<DomainNotification> handler, IUserRepository userRepository) : base(handler)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserAsync(AuthenticationRequest request)
    {
        var user = await _userRepository.GetUserAsync(request.Username!);

        AddNotification(AssertionConcern.AssertArgumentNotNull(nameof(user), user, $"Usuário '{request.Username}' não encontrado no sistema."));

        if (!HasCompliance(user!.ValidationResult))
            return null;

        var password = Password.FromHash(user!.Password.Hash, user!.Password.Salt);
        var isPasswordValid = password.Verify(request.Password!);

        AddNotification(AssertionConcern.AssertArgumentTrue(nameof(user), isPasswordValid, "Senha incorreta."));

        return HasCompliance(user!.ValidationResult) ? user : null;
    }

    public async Task ChangePasswordAsync(ChangePasswordRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        AddNotification(AssertionConcern.AssertArgumentNotNull(nameof(user), user, "Usuário não encontrado no sistema."));

        if (user == null || !HasCompliance(user!.ValidationResult))
            return;

        AddNotification(AssertionConcern.AssertArgumentTrue(nameof(user), user.Password.Verify(request.CurrentPassword), "A senha atual está incorreta."));

        if (!HasCompliance(user!.ValidationResult))
            return;

        var newPassword = Password.Create(request.NewPassword);

        if (!HasCompliance(user!.ValidationResult))
            return;

        user.ChangePassword(newPassword);

        if (HasCompliance(user.ValidationResult))
        {
            await _userRepository.UpdateAsync(user);
        }
    }
}
