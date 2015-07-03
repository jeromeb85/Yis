using System;
using System.Data;
using System.Data.Entity;
using Yis.Framework.Data.Contract;

namespace Yis.Framework.Data.EntityFramework
{
    public class DataContextBase : DbContext, IDataContext
    {
        #region Constructors + Destructors

        public DataContextBase(string nameOrConnection)
            : base(nameOrConnection)
        {
        }

        public DataContextBase()
            : base()
        {
        }

        #endregion Constructors + Destructors

        #region Properties

        public bool IsInTransaction
        {
            get { return Transaction != null; }
        }

        protected IDbTransaction Transaction { get; set; }

        #endregion Properties

        #region Methods

        public void BeginTransaction(System.Data.IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (Transaction != null)
            {
                const string error = "Cannot begin a new transaction while an existing transaction is still running. " +
                               "Please commit or rollback the existing transaction before starting a new one.";

                throw new InvalidOperationException(error);
            }

            OpenConnection();
            Transaction = Database.Connection.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            if (Transaction == null)
            {
                const string error = "Cannot commit a transaction when there is no transaction running.";
                throw new InvalidOperationException(error);
            }

            try
            {
                SaveChanges();
                Transaction.Commit();
                ReleaseTransaction();
            }
            catch (Exception)
            {
                RollBackTransaction();
                throw;
            }
        }

        public void RollBackTransaction()
        {
            if (Transaction == null)
            {
                const string error = "Cannot roll back a transaction when there is no transaction running.";

                throw new InvalidOperationException(error);
            }

            Transaction.Rollback();
            ReleaseTransaction();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        /// <summary>
        /// Opens the connection to the database.
        /// </summary>
        protected virtual void OpenConnection()
        {
            //var objectContext = DbContext.GetObjectContext();
            if (Database.Connection.State != ConnectionState.Open)
            {
                Database.Connection.Open();
            }
        }

        /// <summary>
        /// Releases the transaction.
        /// </summary>
        protected virtual void ReleaseTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
        }

        #endregion Methods
    }
}