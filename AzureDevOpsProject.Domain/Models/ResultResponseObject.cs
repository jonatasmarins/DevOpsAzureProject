using AzureDevOpsProject.Domain.Model;
using Flunt.Notifications;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevOpsProject.Domain.Models
{
    public class ResultResponseObject<T> : IResultResponse
    {
        public bool Success => this.ErrorMessages == null || !this.ErrorMessages.Any();

        public IEnumerable<Notification> ErrorMessages { get; set; }

        public T Value { get; set; }
    }
}
