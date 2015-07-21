using System.Data;

namespace Yis.Framework.Data.Contract
{
    public interface IDataContext
    {
        #region Fields

        bool IsInTransaction { get; }

        #endregion Fields

        #region Methods

        void SaveChanges();

        /// <summary>
        /// Begins a new transaction on the unit of work.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Rolls back all the changes inside a transaction.
        /// </summary>
        void RollBackTransaction();

        /// <summary>
        /// Commits all the changes inside a transaction.
        /// </summary>
        void CommitTransaction();

        #endregion Methods
    }
}