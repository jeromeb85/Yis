using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Erp.Mdm.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Erp.Mdm.Data.Memory
{
    public class MdmDataContext : DataContextBase, IMdmDataContext
    {
        #region Constructors

        public MdmDataContext()
            : base(@"c:\Data\Erp\")
        {
        }

        #endregion Constructors
    
    }
}
