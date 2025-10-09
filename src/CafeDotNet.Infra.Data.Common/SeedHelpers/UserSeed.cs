using CafeDotNet.Core.Users.Entities;
using CafeDotNet.Core.Users.ValueObjects;
using CafeDotNet.Infra.Data.Common.Context;

namespace CafeDotNet.Infra.Data.Common.SeedHelpers;

public static class UserSeed
{
    public static async Task SeedAsync(CafeDbContext dbContext)
    {
        if (!dbContext.Users.Any())
        {
            var admin = new User("admin", Password.Create("C@f3*Net"));
            dbContext.Users.Add(admin);
           
            await dbContext.SaveChangesAsync();
        }
    }
}
