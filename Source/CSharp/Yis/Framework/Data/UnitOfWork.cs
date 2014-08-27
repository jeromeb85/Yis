using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Fluent;
using Yis.Framework.Core.IoC;

namespace Yis.Framework.Data
{
    /// <summary>
    /// Implementation of the unit of work pattern for entity framework.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        #region Fields
        private bool _disposed;
        #endregion

        #region Constructors
        public UnitOfWork(IDataContext context)
        {
            Argument.IsNotNull("context", context);

            Context = context;            
        }

        public UnitOfWork(string nameDataContext)
            : this(DependencyResolver.Resolve<IDataContext>(nameDataContext))
        {

        }
        #endregion

        #region Properties
        protected IDataContext Context { get; private set; }

        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// Gets a value indicating whether this instance is currently in a transaction.
        /// </summary>
        /// <value><c>true</c> if this instance is currently in a transaction; otherwise, <c>false</c>.</value>
        public bool IsInTransaction
        {
            get { return Context.IsInTransaction; }
        }

        /// <summary>
        /// Begins a new transaction on the unit of work.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <exception cref="InvalidOperationException">A transaction is already running.</exception>
        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            Context.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// Rolls back all the changes inside a transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">No transaction is currently running.</exception>
        public virtual void RollBackTransaction()
        {
            Context.RollBackTransaction();
        }

        /// <summary>
        /// Commits all the changes inside a transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">No transaction is currently running.</exception>
        public virtual void CommitTransaction()
        {
            Context.CommitTransaction();
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
            return GetRepository<TRepository>(Context);

        }

        public static TRepository GetRepository<TRepository>(IDataContext context)
        {
            if (!DependencyResolver.IsRegistered<TRepository>())
            {
                string error = string.Format("The specified repository type '{0}' cannot be found. Make sure it is registered in the ServiceLocator.", typeof(TRepository).FullName);
                throw new NotSupportedException(error);
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("dataContext", context);
            return DependencyResolver.Resolve<TRepository>(param);
        }

        /// <summary>
        /// Saves the changes inside the unit of work.
        /// </summary>
        /// <param name="saveOptions">The save options.</param>
        /// <exception cref="InvalidOperationException">A transaction is running. Call CommitTransaction instead.</exception>
        public virtual void SaveChanges()
        {
             if (IsInTransaction)
            {
                const string error = "A transaction is running. Call CommitTransaction instead.";
                throw new InvalidOperationException(error);
            }

             Context.SaveChanges();
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

        #endregion


    }
}
