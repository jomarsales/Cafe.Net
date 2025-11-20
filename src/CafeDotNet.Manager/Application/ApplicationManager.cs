using CafeDotNet.Core.Base.ValueObjects;
using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Core.Validation;
using CafeDotNet.Infra.Data.Common.Interfaces;
using System.Reflection.Metadata;

namespace CafeDotNet.Manager.Application
{
    public class ApplicationManager
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IHandler<DomainNotification> _handler;

        public ApplicationManager(IUnitOfWork unitOfWork, IHandler<DomainNotification> notifications)
        {
            _unitOfWork = unitOfWork;           
            _handler = notifications;
        }

        protected async Task<bool> CommitAsync()
        {
            if (_handler != null)
            {
                if (_handler.HasNotifications(TypeNotification.Error))
                {
                    return false;
                }
            }

            var affetecEntries = await _unitOfWork.CommitAsync();
            
            return affetecEntries > 0;
        }

        protected void AddNotification(ValidationError? error)
        {
            if (error?.Message == null)
                return;

            var domainNotification = new DomainNotification(error.Name, error.Message);

            DomainEvent.Raise(domainNotification, _handler);
        }
    }
}
