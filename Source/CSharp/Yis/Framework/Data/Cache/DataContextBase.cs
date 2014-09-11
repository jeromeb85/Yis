using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Data.Contract;

namespace Yis.Framework.Data.Cache
{
    public class DataContextBase : IDataContext

//        public ICacheStorage< MyProperty { get; set; }
    {
        #region Properties

        public bool IsInTransaction
        {
            get { throw new NotImplementedException(); }
        }

        public int MyProperty { get; set; }

        #endregion Properties

        #region Methods

        public void BeginTransaction(System.Data.IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void RollBackTransaction()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}