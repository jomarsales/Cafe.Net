using CafeDotNet.Core.Users.Entities;
using CafeDotNet.Core.Users.ValueObjects;

namespace CafeDotNet.Core.Users.Interfaces;

public interface IUserService
{
    Task<User?> GetUserAsync(string username, string plainPassword);
}
