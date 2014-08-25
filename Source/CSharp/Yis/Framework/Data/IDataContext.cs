using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Data
{
    public interface IDataContext
    {
        #region Properties
        bool IsInTransaction { get; }
        #endregion

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
        #endregion
    }
}
