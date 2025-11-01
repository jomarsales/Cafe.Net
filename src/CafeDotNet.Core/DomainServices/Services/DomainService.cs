using CafeDotNet.Core.Base.ValueObjects;
using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.Validation;

namespace CafeDotNet.Core.DomainServices.Services;

public class DomainService
{
    protected readonly IHandler<DomainNotification> _handler;

    protected DomainService(IHandler<DomainNotification> handler)
    {
        _handler = handler;
    }

    protected bool HasCompliance(ValidationResult? validationResult)
    {
        if (validationResult == null)
            return true;
       
        var notifications = validationResult.Errors.Where(v => v != null && v?.Message != null).Select(v => new DomainNotification(v!.ToString()!, v.Message)).ToList();
        
        if (!notifications.Any())
            return true;
        
        notifications.ToList().ForEach(x => DomainEvent.Raise(x, _handler));
        
        return false;
    }

    protected void AddNotification(ValidationError? error)
    {
        if (error?.Message == null)
            return;

        var domainNotification = new DomainNotification(error.Name, error.Message);
       
        DomainEvent.Raise(domainNotification, _handler);
    }

    protected void AddNotification<T>(T model, string message, TypeNotification typeNotification = TypeNotification.Error)
    {
        if (string.IsNullOrEmpty(message) && model == null)
            return;

        var domainNotification = new DomainNotification(nameof(model), message, typeNotification);
      
        DomainEvent.Raise(domainNotification, _handler);
    }
}
