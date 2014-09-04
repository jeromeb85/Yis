using System;
using System.Configuration;
using System.Data;
using Yis.Framework.Data.Contract;

namespace Yis.Framework.Data.Database
{
    public class DataContextBase : IDataContext
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public DataContextBase(string nameOrConnection)
        {
            string providerName = ConfigurationManager.ConnectionStrings[nameOrConnection].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings[nameOrConnection].ConnectionString;

            if (!string.IsNullOrEmpty(connectionString))
            {
                _connection = new ConnectionFactory(providerName, connectionString).DbConnection;
            }
        }

        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        /// <summary>
        /// Releases the current transaction
        /// </summary>
        private void ReleaseCurrentTransaction()
        {
            if (_transaction == null)
            {
                return;
            }
            _transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            ReleaseCurrentTransaction();
            _connection.Dispose();
        }

        public void BeginTransaction()
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public bool IsInTransaction
        {
            get { return _transaction != null; }
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction(isolationLevel);
        }

        public void RollBackTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            try
            {
                if (IsInTransaction)
                {
                    _transaction.Commit();
                }
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
        }
    }
}