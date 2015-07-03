using System.Data;

namespace Yis.Framework.Data.Contract
{
    public interface IDataContext
    {
        #region Properties

        bool IsInTransaction { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Begins a new transaction on the unit of work.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Commits all the changes inside a transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rolls back all the changes inside a transaction.
        /// </summary>
        void RollBackTransaction();

        void SaveChanges();

        #endregion Methods
    }
}