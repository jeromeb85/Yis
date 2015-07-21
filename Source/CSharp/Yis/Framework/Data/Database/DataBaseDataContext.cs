namespace Yis.Framework.Data.Database
{
    public class DatabaseDataContext : DataContextBase
    {
        public DatabaseDataContext(string nameOrConnection)
            : base(nameOrConnection)
        {
        }
    }
}