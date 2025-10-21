using CafeDotNet.Core.Users.Entities;

namespace CafeDotNet.Core.Users.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string username);
}
