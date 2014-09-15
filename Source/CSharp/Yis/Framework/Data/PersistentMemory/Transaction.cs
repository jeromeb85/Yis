using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Data.PersistentMemory
{
    internal enum TransactionType
    {
        Add,
        Update,
        Delete
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