using AzureDevOpsProject.Shared.Commands;
using Flunt.Notifications;

namespace AzureDevOpsProject.Domain.Command
{
    public class GetWorkItemCommand : Notifiable, ICommand
    {
        public string Type { get; set; }
        public void Validate()
        {
        }
    }
}
