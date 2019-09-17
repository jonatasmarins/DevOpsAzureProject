using Flunt.Notifications;
using System.Collections.Generic;

namespace AzureDevOpsProject.Domain.Model
{
    public interface IResultResponse
    {
        bool Success { get; }
        IEnumerable<Notification> ErrorMessages { get; set; }
    }
}
