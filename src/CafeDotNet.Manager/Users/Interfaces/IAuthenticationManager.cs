using CafeDotNet.Core.Users.DTOs;

namespace CafeDotNet.Manager.Users.Interfaces;

public interface IAuthenticationManager
{
    public Task<AuthenticationResponse> AuthenticateUserAsyn(AuthenticationRequest request);
    public Task<bool> ChangePasswordAsync(ChangePasswordRequest model);
}
