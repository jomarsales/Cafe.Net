using CafeDotNet.Core.Users.DTOs;
using CafeDotNet.Core.Users.Interfaces;
using CafeDotNet.Manager.Users.Interfaces;

namespace CafeDotNet.Manager.Users.Services;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly IUserService _userService;

    public AuthenticationManager(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<AuthenticationResponse> AuthenticateUserAsyn(AuthenticationRequest request)
    {
        var user = await _userService.GetUserAsync(request.Username, request.Password);  

        return new AuthenticationResponse
        {
            Username = user?.Username,
            Role = user?.Role ?? Core.Users.ValueObjects.RoleType.None
        };
    }
}
