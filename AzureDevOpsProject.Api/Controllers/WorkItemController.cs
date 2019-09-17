using AzureDevOpsProject.ApplicationService;
using AzureDevOpsProject.Domain.Command;
using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.Model;
using AzureDevOpsProject.Domain.Models;
using AzureDevOpsProject.Domain.Services.Inteface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace AzureDevOpsProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class WorkItemController : BaseController
    {
        private readonly IWorkItemApplicationService _workItemApplicationService;

        public WorkItemController(
            IWorkItemApplicationService workItemApplicationService
        )
        {
            _workItemApplicationService = workItemApplicationService;
        }

        /// <summary>
        /// Get All Work Items by type 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(string type)
        {
            var command = new GetWorkItemCommand();
            command.Type = type;

            ResultResponseObject<IEnumerable<WorkItem>> WorkItem = await _workItemApplicationService.Get(command);

            return Response(WorkItem);
        }
    }
}