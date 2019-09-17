using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.ValueObjects;
using Xunit;

namespace AzureDevOpsProject.Test.Entities
{
    public class ProjectTest
    {
        [Fact]
        public void Objeto_Project_Valid()
        {
            Project project = new Project(new Url("https://dev.azure.com/PaPum-Project"), new Name("PaPum-BackEnd"), "79587349734859");
            project.Validate();

            Assert.True(project.Valid);
        }

        [Fact]
        public void Objeto_Project_Url_Name_Empty_Erro()
        {
            Project project = new Project(new Url("https://dev.azure.com/PaPum-Project"), new Name(""), "47973948729482");

            project.Validate();

            Assert.True(project.Invalid);

            Project project1 = new Project();
            project1.Name = new Name();
            project1.Url = new Url("");
            project1.PersonalAccessToken = "";
            project1.Validate();

            Assert.True(project1.Invalid);
        }
    }
}
