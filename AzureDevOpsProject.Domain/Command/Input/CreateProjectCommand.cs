using AzureDevOpsProject.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace AzureDevOpsProject.Domain.Command
{
    public class CreateProjectCommand : Notifiable, ICommand
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string PersonalAccessToken { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsUrlOrEmpty(Url, "Url", "Valor inválido ou vazio")
                .IsNotNullOrWhiteSpace(Name, "Name", "Valor não pode ser vazio")
                .IsNotNullOrWhiteSpace(PersonalAccessToken, "Personal Access Token", "Valor não pode ser vazio")
            );
        }
    }
}
