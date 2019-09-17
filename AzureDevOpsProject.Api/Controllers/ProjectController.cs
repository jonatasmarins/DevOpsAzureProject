using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureDevOpsProject.Domain.Command;
using AzureDevOpsProject.Domain.Command.OutPut;
using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.Models;
using AzureDevOpsProject.Domain.Services.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevOpsProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IProjectApplicationService _projectApplicationService;

        public ProjectController(
            IProjectApplicationService projectApplicationService
        )
        {
            _projectApplicationService = projectApplicationService;
        }

        /// <summary>
        /// Get Project information
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(string type)
        {
            ResultResponseObject<ProjectCommandResult> project = await _projectApplicationService.Get();

            return Response(project);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(CreateProjectCommand project)
        {
            ResultResponse result = new ResultResponse();

            project.Validate();

            if (project.Valid)
            {
                result = await _projectApplicationService.Create(project);
            }
            else
            {
                result.ErrorMessages = project.Notifications;
            }


            return Response(result);
        }
    }
}