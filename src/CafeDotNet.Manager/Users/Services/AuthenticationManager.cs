using AutoMapper;
using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Core.Users.DTOs;
using CafeDotNet.Core.Users.Interfaces;
using CafeDotNet.Infra.Data.Common.Interfaces;
using CafeDotNet.Manager.Application;
using CafeDotNet.Manager.Users.Interfaces;

namespace CafeDotNet.Manager.Users.Services;

public class AuthenticationManager : ApplicationManager, IAuthenticationManager
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthenticationManager(
        IUnitOfWork unitOfWork,
        IHandler<DomainNotification> notifications, 
        IUserService userService, 
        IMapper mapper) : 
        base(unitOfWork, notifications)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse> AuthenticateUserAsyn(AuthenticationRequest request)
    {
        var user = await _userService.GetUserAsync(request);

        return _mapper.Map<AuthenticationResponse>(user);
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordRequest model)
    {
        await _userService.ChangePasswordAsync(model);

        return await CommitAsync();
    }
}
