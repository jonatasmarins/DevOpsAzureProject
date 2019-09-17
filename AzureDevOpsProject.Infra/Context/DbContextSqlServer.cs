using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace AzureDevOpsProject.Infra.Context
{
    public class DbContextSqlServer : DbContext, IDisposable
    {
        private const string CONNECTION_STRING_NAME = "DefaultConnection";
        private readonly IConfiguration _configuration;

        public IDbConnection _connection = null;
        public IDbTransaction _transaction = null;
        public string _connectionString = null;
        public Guid _id = Guid.Empty;

        public DbContextSqlServer(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(CONNECTION_STRING_NAME);   
        }

        #region Proprieties

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        #endregion

        #region Methods

        public void Commit()
        {
            _transaction.Commit();
            Dispose(true);
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose(true);
        }

        #endregion

        #region Dispose

        public bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _connection.Dispose();
                    _connection = null;

                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                    }
                    _transaction = null;
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            if (_transaction != null && _connection.State == ConnectionState.Open)
            {
                _transaction.Commit();
            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
