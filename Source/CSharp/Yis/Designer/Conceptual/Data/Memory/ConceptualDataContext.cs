using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Designer.Software.Data.Memory
{
    public class SoftwareDataContext : DataContextBase, ISoftwareDataContext
    {
        #region Constructors

        public SoftwareDataContext()
            : base(@"D:\DataYis\Software\")
        {
        }

        #endregion Constructors
    }
}