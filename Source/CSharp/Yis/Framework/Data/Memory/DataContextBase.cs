using System;
using System.Collections.Generic;
using System.Data;
using Yis.Framework.Core.Extension;
using Yis.Framework.Data.Contract;

namespace Yis.Framework.Data.Memory
{
    public class DataContextBase : IDataContext
    {
        #region Constructors + Destructors

        public DataContextBase(string path = null)
        {
            Path = path;
        }

        #endregion Constructors + Destructors

        #region Fields

        private readonly string Path = String.Empty;

        private DataStore _store;

        private Stack<Queue<Transaction>> _transaction;

        #endregion Fields

        #region Properties

        public bool IsInTransaction
        {
            get { return Transaction.Count != 0; }
        }

        private DataStore Store
        {
            get
            {
                if (_store.IsNull())
                {
                    _store = new DataStore(Path);
                }
                return _store;
            }
        }

        private Stack<Queue<Transaction>> Transaction
        {
            get
            {
                if (_transaction.IsNull())
                {
                    _transaction = new Stack<Queue<Transaction>>();
                }

                return _transaction;
            }
        }

        #endregion Properties

        #region Methods

        public void BeginTransaction(System.Data.IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            Transaction.Push(new Queue<Transaction>());
        }

        public void CommitTransaction()
        {
            try
            {
                Queue<Transaction> listTransaction = Transaction.Peek();

                foreach (Transaction tr in listTransaction)
                {
                    if (tr.Type == TransactionType.Add)
                        Store.Save(tr.Obj);

                    if (tr.Type == TransactionType.Delete)
                        Store.Remove(tr.Obj);

                    if (tr.Type == TransactionType.Update)
                    {
                        Store.Save(tr.Obj);
                    }
                }

                listTransaction.Clear();
            }
            catch
            {
                RollBackTransaction();
            }
        }

        public IEnumerable<TEntity> Get<TEntity>()
        {
            return Store.List<TEntity>();
        }

        public void RollBackTransaction()
        {
            Transaction.Pop();
        }

        public void SaveChanges()
        {
            if (IsInTransaction)
                CommitTransaction();
        }

        internal void Add<TEntity>(TEntity entity)
        {
            if (!IsInTransaction)
            {
                BeginTransaction();
            }

            Transaction.Peek().Enqueue(new Transaction(entity, TransactionType.Add));
        }

        internal void Remove<TEntity>(TEntity entity)
        {
            if (!IsInTransaction)
            {
                BeginTransaction();
            }

            Transaction.Peek().Enqueue(new Transaction(entity, TransactionType.Delete));
        }

        internal void Update<TEntity>(TEntity entity)
        {
            if (!IsInTransaction)
            {
                BeginTransaction();
            }

            Transaction.Peek().Enqueue(new Transaction(entity, TransactionType.Update));
        }

        #endregion Methods
    }
}