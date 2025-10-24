using CafeDotNet.Core.Base.ValueObjects;

namespace CafeDotNet.Core.DomainServices.Interfaces
{
    public interface IHandler<T> : IDisposable where T : IDomainEvent
    {
        void Handle(T args);
        Task HandleAsync(T args);
        IEnumerable<T> Nofity();
        bool HasNotifications(TypeNotification? typeNotification = null);
        List<T> GetValues();
    }
}
