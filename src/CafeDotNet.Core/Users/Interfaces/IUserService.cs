using CafeDotNet.Core.Users.DTOs;
using CafeDotNet.Core.Users.Entities;

namespace CafeDotNet.Core.Users.Interfaces;

public interface IUserService
{
    Task ChangePasswordAsync(ChangePasswordRequest model);
    Task<User?> GetUserAsync(AuthenticationRequest request);
}
