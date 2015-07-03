using Yis.Erp.Mdm.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Erp.Mdm.Data.Memory
{
    public class MdmDataContext : DataContextBase, IMdmDataContext
    {
        #region Constructors + Destructors

        public MdmDataContext()
            : base(@"c:\Data\Erp\")
        {
        }

        #endregion Constructors + Destructors
    }
}