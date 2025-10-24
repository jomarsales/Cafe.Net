using CafeDotNet.Core.Base.ValueObjects;
using CafeDotNet.Core.DomainServices.Interfaces;
namespace CafeDotNet.Core.DomainServices.Services;

public class DomainNotificationHandler : IHandler<DomainNotification>
{
    private List<DomainNotification> _notifications;

    public DomainNotificationHandler()
    {
        _notifications = new List<DomainNotification>();
    }

    public void Handle(DomainNotification args)
    {
        _notifications.Add(args);
    }

    public IEnumerable<DomainNotification> Nofity()
    {
        return GetValues();
    }

    public List<DomainNotification> GetValues()
    {
        return _notifications;
    }

    public bool HasNotifications(TypeNotification? typeNotification = null)
    {
        if (typeNotification.HasValue)
            return GetValues().Any(x => x.TypeNotification == typeNotification.Value);
        else
            return GetValues().Count > 0;
    }

    public void Dispose()
    {
        _notifications = new List<DomainNotification>();
    }

    public Task HandleAsync(DomainNotification args)
    {
        _notifications.Add(args);
        
        return Task.FromResult(0);
    }
}
