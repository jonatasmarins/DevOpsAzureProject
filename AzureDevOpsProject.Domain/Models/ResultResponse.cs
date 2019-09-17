using System.Collections.Generic;
using System.Linq;
using AzureDevOpsProject.Domain.Model;
using Flunt.Notifications;

namespace AzureDevOpsProject.Domain.Models
{
    public class ResultResponse : IResultResponse
    {
        public bool Success => this.ErrorMessages == null || !this.ErrorMessages.Any();

        public IEnumerable<Notification> ErrorMessages { get; set; }
    }
}
