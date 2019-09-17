using AzureDevOpsProject.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureDevOpsProject.Api.Controllers
{
    public class BaseController : Controller
    {
        protected new IActionResult Response(IResultResponse resultResponse = null)
        {
            if (resultResponse != null && resultResponse.Success)
            {
                return Ok(resultResponse);
            }
            else
            {
                return BadRequest(resultResponse.ErrorMessages);
            }
        }        
    }
}
