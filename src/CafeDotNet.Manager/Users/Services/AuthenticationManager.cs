using AutoMapper;
using CafeDotNet.Core.Users.DTOs;
using CafeDotNet.Core.Users.Interfaces;
using CafeDotNet.Manager.Users.Interfaces;

namespace CafeDotNet.Manager.Users.Services;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthenticationManager(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse> AuthenticateUserAsyn(AuthenticationRequest request)
    {
        var user = await _userService.GetUserAsync(request);

        return _mapper.Map<AuthenticationResponse>(user);
    }
}
