using AzureDevOpsProject.Domain.Command;
using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDevOpsProject.Domain.Services.Inteface
{
    public interface IWorkItemApplicationService
    {
        Task<ResultResponseObject<IEnumerable<WorkItem>>> Get(GetWorkItemCommand command);
    }
}
