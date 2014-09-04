using System;
using System.Data;
using System.Data.Entity;
using Yis.Framework.Core.Helper;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Logging.Contract;
using Yis.Framework.Data.Contract;

namespace Yis.Framework.Data
{
    /// <summary>
    /// Implementation of the unit of work pattern for entity framework.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private bool _disposed;

        private static IServiceLocator _locator;

        protected static IServiceLocator Locator
        {
            get
            {
                if (_locator.IsNull()) _locator = Resolver.Resolve<IServiceLocator>();
                return _locator;
            }
        }

        private static ILog _log;

        protected static ILog Log
        {
            get
            {
                if (_log.IsNull()) _log = Resolver.Resolve<ILog>();
                return _log;
            }
        }

        protected static IDependencyResolver Resolver
        {
            get { return DependencyResolverManager.Default; }
        }

        #endregion Fields

        #region Constructors

        public UnitOfWork(IDataContext context)
        {
            ArgumentHelper.IsNotNull("context", context);

            Context = context;
        }

        public UnitOfWork(string nameDataContext)
            : this(Resolver.Resolve<IDataContext>(nameDataContext))
        {
        }

        #endregion Constructors

        #region Properties

        protected IDataContext Context { get; private set; }

        #endregion Properties

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
            if (!Resolver.IsRegistered<TRepository>())
            {
                var obj = Locator.ResolveAndCreateType<TRepository>(new object[] { context });

                if (obj.IsNull())
                {
                    string error = string.Format("Le Provider '{0}' n'a pu être trouvé.", typeof(TRepository).FullName);
                    Log.Error(error);
                    throw new NotSupportedException(error);
                }

                Resolver.Register<TRepository>(obj);
            }

            return Resolver.Resolve<TRepository>();
        }

        /// <summary>
        /// Saves the changes inside the unit of work.
        /// </summary>
        public virtual void SaveChanges()
        {
            if (IsInTransaction)
            {
                CommitTransaction();
            }
            else
            {
                Context.SaveChanges();
            }
        }

        #endregion IUnitOfWork Members

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

        #endregion Implementation of IDisposable
    }
}