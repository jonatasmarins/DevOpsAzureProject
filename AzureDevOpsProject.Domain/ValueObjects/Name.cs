using AzureDevOpsProject.Shared.ValueObjects;
using Flunt.Validations;

namespace AzureDevOpsProject.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name()
        {

        }

        public Name(string nameProject)
        {
            NameProject = nameProject;
        }

        public override string ToString()
        {
            return NameProject;
        }

        public string NameProject { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(NameProject, "Name", "Não pode ser vazio")
           );
        }
    }
}
