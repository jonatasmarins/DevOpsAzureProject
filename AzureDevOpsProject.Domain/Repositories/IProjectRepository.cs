using AzureDevOpsProject.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDevOpsProject.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<bool> ProjectExists();
        Task<Project> Get();
        Task Create(Project project);
        Task Update(Project project);
    }
}
