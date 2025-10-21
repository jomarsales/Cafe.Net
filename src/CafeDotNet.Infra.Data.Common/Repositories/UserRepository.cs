using CafeDotNet.Core.Users.Entities;
using CafeDotNet.Core.Users.Interfaces;
using CafeDotNet.Infra.Data.Common.Context;
using Microsoft.EntityFrameworkCore;

namespace CafeDotNet.Infra.Data.Common.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CafeDbContext _cafeDbContext;

    public UserRepository(CafeDbContext cafeDbContext)
    {
        _cafeDbContext = cafeDbContext;
    }

    public async Task<User?> GetUserAsync(string username)
    {
        return await _cafeDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}
