using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Caching;
using Yis.Framework.Core.Caching.Contract;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.Helper;
using Yis.Framework.Data.Contract;
using Yis.Framework.Model.Contract;

namespace Yis.Framework.Data.Memory
{
    public class DataContextBase : IDataContext
    {
        #region Fields

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
                    _store = new DataStore();
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

        public void Remove<TEntity>(TEntity entity)
        {
            if (!IsInTransaction)
            {
                BeginTransaction();
            }

            Transaction.Peek().Enqueue(new Transaction(entity, TransactionType.Delete));
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

        public void Update<TEntity>(TEntity entity)
        {
            if (!IsInTransaction)
            {
                BeginTransaction();
            }

            Transaction.Peek().Enqueue(new Transaction(entity, TransactionType.Update));
        }

        private void Add<TEntity>(TEntity entity)
        {
            if (!IsInTransaction)
            {
                BeginTransaction();
            }

            Transaction.Peek().Enqueue(new Transaction(entity, TransactionType.Add));
        }

        #endregion Methods
    }
}