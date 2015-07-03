namespace Yis.Framework.Data.Database
{
    public class DatabaseDataContext : DataContextBase
    {
        #region Constructors + Destructors

        public DatabaseDataContext(string nameOrConnection)
            : base(nameOrConnection)
        {
        }

        #endregion Constructors + Destructors
    }
}