using Flunt.Notifications;

namespace AzureDevOpsProject.Shared.ValueObjects
{
    public abstract class ValueObject : Notifiable
    {
        public abstract void Validate();
    }
}
