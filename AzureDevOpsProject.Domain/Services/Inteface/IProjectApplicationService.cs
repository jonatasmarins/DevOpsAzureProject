using AzureDevOpsProject.Domain.Command;
using AzureDevOpsProject.Domain.Command.OutPut;
using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.Models;
using System.Threading.Tasks;

namespace AzureDevOpsProject.Domain.Services.Inteface
{
    public interface IProjectApplicationService
    {
        Task<ResultResponseObject<ProjectCommandResult>> Get();
        Task<ResultResponse> Create(CreateProjectCommand project);
    }
}
