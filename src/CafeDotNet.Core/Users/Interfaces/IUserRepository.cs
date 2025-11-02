using CafeDotNet.Core.Base.Repositories;
using CafeDotNet.Core.Users.Entities;

namespace CafeDotNet.Core.Users.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserAsync(string username);
}
