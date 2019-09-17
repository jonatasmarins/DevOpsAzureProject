using AzureDevOpsProject.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace AzureDevOpsProject.ApplicationService.Base
{
    public class ApplicationServiceBase
    {
        private IUnitOfWork _unitOfWork;
        private IConfiguration configuration;
        //private IHandler<DomainNotification> _notifications;

        public ApplicationServiceBase(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            //this._notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
        }

        //public bool Commit()
        //{
        //    //if (_notifications.HasNotifications())
        //    //    return false;

        //    //_unitOfWork.Commit();
        //    //return true;
        //}
    }
}
