using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureDevOpsProject.Domain.Command;
using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.Repositories;
using Dapper;

namespace AzureDevOpsProject.Infra.Repositories
{
    public class WorkItemRepository : IWorkItemRepository
    {
        IUnitOfWork _uow;

        public WorkItemRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #region Get

        public async Task<IEnumerable<WorkItem>> Get(GetWorkItemCommand getWorkItemCommand)
        {
            StringBuilder sql = new StringBuilder(
                @"SELECT * FROM  WorkItem"
            );

            if (!string.IsNullOrWhiteSpace(getWorkItemCommand.Type))
            {
                sql.Append(" WHERE Type = @Type ");
            }

            return await _uow.Connection.QueryAsync<WorkItem>(sql.ToString(), new { getWorkItemCommand.Type }, _uow.Transaction);
        }

        public async Task<bool> WorkItemExists(WorkItem workItem)
        {
            StringBuilder sql = new StringBuilder(
                @"SELECT Count(1) COUNT FROM Project WHERE Code = @Code AND Id = @Id"
            );

            return await _uow.Connection.QueryFirstOrDefaultAsync<bool>(sql.ToString(), new { workItem }, _uow.Transaction);
        }

        #endregion

        #region Insert

        public async Task Create(WorkItem workItem)
        {
            StringBuilder sql = new StringBuilder(
                @"INSERT INTO WorkItem (Id, Code, Type, Title, CreateDate)
                  VALUES (@Id, @Code, @Type, @Title, @CreateDate)"
            );

            await _uow.Connection.ExecuteAsync(sql.ToString(), new { workItem.Id, workItem.Code, workItem.Type, workItem.Title, workItem.CreateDate  }, _uow.Transaction);
        }

        public async Task Creates(IEnumerable<WorkItem> workItens)
        {
            foreach (var item in workItens)
            {
                await Create(item);
            }
        }

        #endregion

        #region Delete

        public async Task Delete()
        {
            StringBuilder sql = new StringBuilder(
                @"DELETE WorkItem"
            );

            await _uow.Connection.ExecuteAsync(sql.ToString(), null, _uow.Transaction);
        }

        #endregion
    }
}
