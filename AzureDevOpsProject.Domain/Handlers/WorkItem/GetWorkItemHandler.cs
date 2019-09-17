using AzureDevOpsProject.Domain.Command;
using AzureDevOpsProject.Domain.Repositories;
using AzureDevOpsProject.Shared.Commands;
using AzureDevOpsProject.Shared.Handlers;
using Flunt.Notifications;

namespace AzureDevOpsProject.Domain.Handlers
{
    public class GetWorkItemHandler : Notifiable, IHandler<GetWorkItemCommand>
    {
        private readonly IWorkItemRepository _workItemRepository;

        public GetWorkItemHandler(IWorkItemRepository workItemRepository)
        {
            _workItemRepository = workItemRepository;
        }

        public ICommandResult Handle(GetWorkItemCommand command)
        {
            _workItemRepository.Get(command);

            return new CommandResult(true, "");
        }
    }
}
