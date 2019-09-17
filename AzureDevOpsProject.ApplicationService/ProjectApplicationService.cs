using System.Threading.Tasks;
using AutoMapper;
using AzureDevOpsProject.ApplicationService.Base;
using AzureDevOpsProject.Domain.Command;
using AzureDevOpsProject.Domain.Command.OutPut;
using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.Models;
using AzureDevOpsProject.Domain.Repositories;
using AzureDevOpsProject.Domain.Services.Inteface;

namespace AzureDevOpsProject.ApplicationService
{
    public class ProjectApplicationService : ApplicationServiceBase, IProjectApplicationService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProjectApplicationService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : base(unitOfWork)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultResponse> Create(CreateProjectCommand projectCommand)
        {
            ResultResponse result = new ResultResponse();

            //Mapper
            //Project project = _mapper.Map<Project>(projectCommand);

            Project project = new Project();
            project.Name = new Domain.ValueObjects.Name(projectCommand.Name);
            project.Url = new Domain.ValueObjects.Url(projectCommand.Url);
            project.PersonalAccessToken = projectCommand.PersonalAccessToken;

            //verify project model request is valid
            project.Validate();
            if (project.Invalid)
            {
                result.ErrorMessages = project.Notifications;

                return result;
            }

            //verify if exist project
            bool existProject;
            using (_uow.Create())
            {
                existProject = await _uow.projectRepository.ProjectExists();
            }
                

            //case exist project infomation, update data and delete work items
            //case project not infomation, create register in database
            if (existProject)
            {
                using (_uow.Create())
                {
                    await _uow.projectRepository.Update(project);
                    await _uow.workItemRepository.Delete();
                }
            }
            else
            {
                using (_uow.Create())
                {
                    await _uow.projectRepository.Create(project);
                }
            }

            return result;
        }

        public async Task<ResultResponseObject<ProjectCommandResult>> Get()
        {
            ResultResponseObject<ProjectCommandResult> result = new ResultResponseObject<ProjectCommandResult>();
            Project project = new Project();
            using (_uow.Create())
            {
                project = await _uow.projectRepository.Get();
            }

            result.Value = _mapper.Map<ProjectCommandResult>(project);

            return result;
        }
    }
}
