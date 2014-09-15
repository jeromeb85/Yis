using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Data.PersistentMemory;

namespace Yis.Designer.Software.Data.MemoryPersistent
{
    public class SoftwareDataContext : DataContextBase, ISoftwareDataContext
    {
        #region Constructors

        public SoftwareDataContext()
            : base(@"E:\DataYis\Software\")
        {
        }

        #endregion Constructors
    }
}