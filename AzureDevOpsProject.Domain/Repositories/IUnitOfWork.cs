using System;
using System.Data;

namespace AzureDevOpsProject.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWork Create();

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        Guid Id { get; }

        #region Repositories

        IProjectRepository projectRepository { get; }
        IWorkItemRepository workItemRepository { get; }

        #endregion
    }
}
