using CafeDotNet.Core.Base.ValueObjects;
using CafeDotNet.Core.DomainServices.Interfaces;
using CafeDotNet.Core.DomainServices.Services;
using CafeDotNet.Infra.Data.Common.Interfaces;

namespace CafeDotNet.Manager.Application
{
    public class ApplicationManager
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IHandler<DomainNotification> Notifications;

        public ApplicationManager(IUnitOfWork unitOfWork, IHandler<DomainNotification> notifications)
        {
            _unitOfWork = unitOfWork;           
            Notifications = notifications;
        }

        protected async Task<bool> CommitAsync()
        {
            if (Notifications != null)
            {
                if (Notifications.HasNotifications(TypeNotification.Error))
                {
                    return false;
                }
            }

            var affetecEntries = await _unitOfWork.CommitAsync();
            
            return affetecEntries > 0;
        }
    }
}
