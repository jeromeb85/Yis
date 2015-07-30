using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Erp.Designer.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Erp.Designer.Data.Memory
{
    public class DesignerDataContext : DataContextBase, IDesignerDataContext
    {
        #region Constructors

        public DesignerDataContext()
            : base(@"c:\Data\Erp\")
        {
        }

        #endregion Constructors
    
    }
}
