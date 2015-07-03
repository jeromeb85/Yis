﻿using System;
using System.Data;

namespace Yis.Framework.Data.Contract
{
    /// <summary>
    /// Définition d'une UnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
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

        TRepository GetRepository<TRepository>()
            where TRepository : IRepository;

        /// <summary>
        /// Rolls back all the changes inside a transaction.
        /// </summary>
        void RollBackTransaction();

        /// <summary>
        /// Saves the changes inside the unit of work.
        /// </summary>
        /// <param name="saveOptions">Options de sauvegarde.</param>
        void SaveChanges();

        #endregion Methods
    }
}