namespace Yis.Framework.Data.Memory
{
    internal enum TransactionType
    {
        Add,
        Update,
        Delete
    }

    internal class Transaction
    {
        #region Constructors + Destructors

        public Transaction(object obj, TransactionType type)
        {
            Obj = obj;
            Type = type;
        }

        #endregion Constructors + Destructors

        #region Fields

        public readonly object Obj;

        public readonly TransactionType Type;

        #endregion Fields
    }
}