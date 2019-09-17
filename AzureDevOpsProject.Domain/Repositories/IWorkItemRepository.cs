using AzureDevOpsProject.Domain.Command;
using AzureDevOpsProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDevOpsProject.Domain.Repositories
{
    public interface IWorkItemRepository
    {
        Task Create(WorkItem workItem);
        Task Creates(IEnumerable<WorkItem> workItens);
        Task<bool> WorkItemExists(WorkItem workItem);
        Task Delete();
        Task<IEnumerable<WorkItem>> Get(GetWorkItemCommand command);
    }
}
