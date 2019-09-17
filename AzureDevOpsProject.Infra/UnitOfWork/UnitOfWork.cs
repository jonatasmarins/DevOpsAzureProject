using AzureDevOpsProject.Domain.Repositories;
using AzureDevOpsProject.Infra.Context;
using AzureDevOpsProject.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AzureDevOpsProject.Infra.UnitOfWork
{
    public class UnitOfWork : DbContextSqlServer, IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        #region Proprieties

        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        public Guid Id
        {
            get { return _id; }
        }
        

        #endregion

        public UnitOfWork(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            Create();
        }

        public IUnitOfWork Create()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                Begin();
                disposed = false;
            }

            return this;
        }

        #region Repositories

        public IProjectRepository projectRepository => new ProjectRepository(this);

        public IWorkItemRepository workItemRepository => new WorkItemRepository(this);

        #endregion
    }
}
