using AzureDevOpsProject.Domain.ValueObjects;
using AzureDevOpsProject.Shared.Entities;
using Dapper.Contrib.Extensions;
using Flunt.Validations;

namespace AzureDevOpsProject.Domain.Entities
{
    [Table("Project")]
    public class Project : Entity
    {
        public Project()
        {

        }

        public Project(Url url, Name name, string personalAccessToken)
        {
            Url = url;
            Name = name;
            PersonalAccessToken = personalAccessToken;
        }

        public Url Url { get; set; }
        public Name Name { get; set; }
        public string PersonalAccessToken { get; set; }

        #region Methods

        public override void Validate()
        {
            Url.Validate();
            Name.Validate();

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(PersonalAccessToken, "PersonalAccessToken", "Não pode ser vazio")
            );

            AddNotifications(Url, Name);
        }

        #endregion
    }
}
