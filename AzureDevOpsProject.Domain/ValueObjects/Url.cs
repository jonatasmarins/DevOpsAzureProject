using AzureDevOpsProject.Shared.ValueObjects;
using Flunt.Validations;

namespace AzureDevOpsProject.Domain.ValueObjects
{
    public class Url : ValueObject
    {
        public Url()
        {

        }

        public Url(string urlProject)
        {
            UrlProject = urlProject;            
        }

        public override string ToString()
        {
            return UrlProject;
        }

        public string UrlProject { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsUrlOrEmpty(UrlProject, "Url", "Url não pode ser vazia")
                .IsUrl(UrlProject, "Url", "Url Invalida")
            );
        }
    }
}
