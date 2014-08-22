using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Data
{

    /// <summary>
    /// Définition d'une UnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region Properties
        bool IsInTransaction { get; }
        #endregion

        #region Methods
        TRepository GetRepository<TRepository>()
            where TRepository : IRepository;

        /// <summary>
        /// Saves the changes inside the unit of work.
        /// </summary>
        /// <param name="saveOptions">Options de sauvegarde.</param>
        void SaveChanges(SaveOptions saveOptions = SaveOptions.DetectChangesBeforeSave | SaveOptions.AcceptAllChangesAfterSave);

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
