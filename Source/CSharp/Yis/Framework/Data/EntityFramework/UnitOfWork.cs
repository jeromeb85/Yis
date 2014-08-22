using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.IoC;

namespace Yis.Framework.Data.EntityFramework
{
    /// <summary>
    /// Implementation of the unit of work pattern for entity framework.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        #region Fields
       // private readonly IServiceLocator _serviceLocator;
        //private readonly ITypeFactory _typeFactory;

        private bool _disposed;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="tag">The tag to uniquely identify this unit of work. If <c>null</c>, a unique id will be generated.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="context" /> is <c>null</c>.</exception>
        public UnitOfWork(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("pas de context donné");
            }
            
            //_serviceLocator = ServiceLocator.Default;
            //_typeFactory = _serviceLocator.ResolveType<ITypeFactory>();

            DbContext = context;
            //Tag = tag ?? UniqueIdentifierHelper.GetUniqueIdentifier<UnitOfWork>().ToString(CultureInfo.InvariantCulture);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the db context.
        /// </summary>
        /// <value>The db context.</value>
        protected DbContext DbContext { get; private set; }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <value>The tag.</value>
      //  protected string Tag { get; private set; }

        /// <summary>
        /// Gets or sets the transaction.
        /// </summary>
        /// <value>The transaction.</value>
        protected IDbTransaction Transaction { get; set; }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// Gets a value indicating whether this instance is currently in a transaction.
        /// </summary>
        /// <value><c>true</c> if this instance is currently in a transaction; otherwise, <c>false</c>.</value>
        public bool IsInTransaction
        {
            get { return Transaction != null; }
        }

        /// <summary>
        /// Begins a new transaction on the unit of work.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <exception cref="InvalidOperationException">A transaction is already running.</exception>
        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {

            if (Transaction != null)
            {
                const string error = "Cannot begin a new transaction while an existing transaction is still running. " +
                               "Please commit or rollback the existing transaction before starting a new one.";

                throw new InvalidOperationException(error);
            }

            OpenConnection();
            Transaction = DbContext.Database.Connection.BeginTransaction(isolationLevel);
            //var objectContext = DbContext.GetObjectContext();
            //Transaction = objectContext.Connection.BeginTransaction(isolationLevel);

        }

        /// <summary>
        /// Rolls back all the changes inside a transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">No transaction is currently running.</exception>
        public virtual void RollBackTransaction()
        {

            if (Transaction == null)
            {
                const string error = "Cannot roll back a transaction when there is no transaction running.";


                throw new InvalidOperationException(error);
            }

            Transaction.Rollback();
            ReleaseTransaction();
        }

        /// <summary>
        /// Commits all the changes inside a transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">No transaction is currently running.</exception>
        public virtual void CommitTransaction()
        {
            if (Transaction == null)
            {
                const string error = "Cannot commit a transaction when there is no transaction running.";
                throw new InvalidOperationException(error);
            }

            try
            {
                DbContext.SaveChanges();
                //var objectContext = DbContext.GetObjectContext();
                //objectContext.SaveChanges();
                Transaction.Commit();
                ReleaseTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Gets the repository that is created specificially for this unit of work.
        /// <para />
        /// Note that the following conditions must be met: <br />
        /// <list type="number">
        /// <item>
        /// <description>
        /// The container must be registered in the <see cref="ServiceLocator" /> as <see cref="RegistrationType.Transient" /> type. If the
        /// repository is declared as non-transient, it will be instantiated as new instance anyway.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The repository must have a constructor accepting a <see cref="DbContext" /> instance.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <typeparam name="TEntityRepository">The type of the entity repository.</typeparam>
        /// <returns>The entity repository.</returns>
        /// <exception cref="NotSupportedException">The specified repository type cannot be found.</exception>
        public virtual TRepository GetRepository<TRepository>()
            where TRepository : IRepository
        {
            
            if (!DependencyResolver.IsRegistered<TRepository>())
            {
                string error = string.Format("The specified repository type '{0}' cannot be found. Make sure it is registered in the ServiceLocator.", typeof(TRepository).FullName);                
                throw new NotSupportedException(error);
            }

            //var repository = _typeFactory.CreateInstanceWithParameters(registrationInfo.ImplementingType, DbContext);
            //return (TRepository) repository;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("dbContext", DbContext);
            return DependencyResolver.Resolve<TRepository>(param);
        }

        /// <summary>
        /// Saves the changes inside the unit of work.
        /// </summary>
        /// <param name="saveOptions">The save options.</param>
        /// <exception cref="InvalidOperationException">A transaction is running. Call CommitTransaction instead.</exception>
        public virtual void SaveChanges(SaveOptions saveOptions = SaveOptions.DetectChangesBeforeSave | SaveOptions.AcceptAllChangesAfterSave)
        {
             if (IsInTransaction)
            {
                const string error = "A transaction is running. Call CommitTransaction instead.";
                throw new InvalidOperationException(error);
            }

             DbContext.SaveChanges();
            //var objectContext = DbContext.GetObjectContext();
            //objectContext.SaveChanges(saveOptions);
        }
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region Methods
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_disposed)
            {
                return;
            }

            OnDisposing();

            _disposed = true;
        }

        /// <summary>
        /// Called when the object is being disposed.
        /// </summary>
        protected virtual void OnDisposing()
        {
        }

        /// <summary>
        /// Disposes the db context.
        /// </summary>
        protected void DisposeDbContext()
        {
            if (DbContext != null)
            {
                DbContext.Dispose();
            }
        }
        #endregion

        #endregion

        #region Methods
        /// <summary>
        /// Opens the connection to the database.
        /// </summary>
        protected virtual void OpenConnection()
        {           
            //var objectContext = DbContext.GetObjectContext();
            if (DbContext.Database.Connection.State != ConnectionState.Open)
            {
                DbContext.Database.Connection.Open();
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
        #endregion
    }
}
