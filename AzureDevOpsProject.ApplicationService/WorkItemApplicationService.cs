using System.Collections.Generic;
using System.Threading.Tasks;
using AzureDevOpsProject.ApplicationService.Base;
using AzureDevOpsProject.Domain.Command;
using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.Models;
using AzureDevOpsProject.Domain.Repositories;
using AzureDevOpsProject.Domain.Services.Inteface;

namespace AzureDevOpsProject.ApplicationService
{
    public class WorkItemApplicationService : ApplicationServiceBase, IWorkItemApplicationService
    {
        private readonly IUnitOfWork _uow;
        public WorkItemApplicationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<ResultResponseObject<IEnumerable<WorkItem>>> Get(GetWorkItemCommand command)
        {
            ResultResponseObject<IEnumerable<WorkItem>> resultReponse = new ResultResponseObject<IEnumerable<WorkItem>>();            

            using (_uow.Create())
            {
                resultReponse.Value = await _uow.workItemRepository.Get(command);
            }            

            return resultReponse;
        }
    }
}
