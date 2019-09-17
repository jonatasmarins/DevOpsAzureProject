using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.Repositories;
using AzureDevOpsProject.Domain.ValueObjects;
using AzureDevOpsProject.Infra.Context;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDevOpsProject.Infra.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IUnitOfWork _uow;
        public ProjectRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Create(Project project)
        {
            StringBuilder sql = new StringBuilder(
                @"INSERT INTO Project (Id, Url, Name, PersonalAccessToken)
                  VALUES (@Id, @Url, @Name, @PersonalAccessToken)"
            );

            await _uow.Connection.ExecuteAsync(sql.ToString(), 
                new {
                        project.Id,
                        Url = project.Url.UrlProject,
                        Name = project.Name.NameProject,
                        project.PersonalAccessToken }, 
                _uow.Transaction
            );
        }

        public async Task<Project> Get()
        {
            StringBuilder sql = new StringBuilder(
                @"SELECT 
                    Id, 
                    PersonalAccessToken,
                    Url [UrlProject], 
                    Name [NameProject] 
                  FROM Project"
            );

            IEnumerable<Project> result = await _uow.Connection.QueryAsync<Project, Url, Name, Project>(
                sql.ToString(),
                (p, u, n) => { p.Url = u; p.Name = n; return p; },
                null,
                _uow.Transaction,
                splitOn: "UrlProject, NameProject"
            );

            return result.FirstOrDefault();
        }

        public async Task<bool> ProjectExists()
        {
            StringBuilder sql = new StringBuilder(
                @"SELECT Count(1) COUNT FROM Project"
            );

            return await _uow.Connection.QueryFirstOrDefaultAsync<bool>(sql.ToString(), null, _uow.Transaction);
        }

        public async Task Update(Project project)
        {
            StringBuilder sql = new StringBuilder(
                @"UPDATE Project 
                  SET Url = @Url,
                      Name = @Name,
                      PersonalAccessToken = @PersonalAccessToken
                "
            );

            await _uow.Connection.ExecuteAsync(sql.ToString(),
                new { Url = project.Url.UrlProject, Name = project.Name.NameProject, project.PersonalAccessToken },
                _uow.Transaction
            );
        }
    }
}
