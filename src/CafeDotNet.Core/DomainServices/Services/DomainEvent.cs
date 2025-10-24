using CafeDotNet.Core.DomainServices.Interfaces;

namespace CafeDotNet.Core.DomainServices.Services;

public static class DomainEvent
{
    public static void Raise<T>(T args, IHandler<T> obj) where T : IDomainEvent
    {
        obj.Handle(args);
    }

    public static async Task RaiseAsync<T>(T args, IHandler<T> obj) where T : IDomainEvent
    {
        await obj.HandleAsync(args);
    }
}
