using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Designer.Software.Data.Memory
{
    public class SoftwareDataContext : DataContextBase, ISoftwareDataContext
    {
        #region Constructors + Destructors

        public SoftwareDataContext()
            : base(@"D:\DataYis\Software\")
        {
        }

        #endregion Constructors + Destructors
    }
}