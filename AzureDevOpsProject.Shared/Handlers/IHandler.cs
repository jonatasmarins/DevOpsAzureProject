using AzureDevOpsProject.Shared.Commands;

namespace AzureDevOpsProject.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T commmand);
    }
}
