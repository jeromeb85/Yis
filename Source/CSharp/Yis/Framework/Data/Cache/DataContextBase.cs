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

namespace Yis.Framework.Data.Cache
{
    internal enum TransactionType
    {
        Add,
        Update,
        Delete
    }

    public class DataContextBase : IDataContext
    {
        #region Fields

        private IDictionary<Type, List<Object>> _cache;

        private Stack<Queue<Transaction>> _transaction;

        #endregion Fields

        #region Properties

        public bool IsInTransaction
        {
            get { return Transaction.Count != 0; }
        }

        private IDictionary<Type, List<Object>> Cache
        {
            get
            {
                if (_cache.IsNull())
                {
                    _cache = new Dictionary<Type, List<Object>>();
                }
                return _cache;
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
                    if (!Cache.ContainsKey(tr.Obj.GetType()))
                        Cache.Add(tr.Obj.GetType(), new List<Object>());

                    if (tr.Type == TransactionType.Add)
                        Cache[tr.Obj.GetType()].Add(tr.Obj);

                    if (tr.Type == TransactionType.Delete)
                        Cache[tr.Obj.GetType()].Remove(tr.Obj);

                    if (tr.Type == TransactionType.Update)
                    {
                        Cache[tr.Obj.GetType()].Remove(tr.Obj);
                        Cache[tr.Obj.GetType()].Add(tr.Obj);
                    }
                }

                listTransaction.Clear();
            }
            catch
            {
                RollBackTransaction();
            }
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

        internal void Get<TEntity>()
        {
            if (!Cache.ContainsKey(typeof(TEntity)))
                Cache.Add(typeof(TEntity), new List<Object>());
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

    internal class Transaction
    {
        #region Fields

        public readonly object Obj;

        public readonly TransactionType Type;

        #endregion Fields

        #region Constructors

        public Transaction(object obj, TransactionType type)
        {
            Obj = obj;
            Type = type;
        }

        #endregion Constructors
    }
}