using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Data.Memory;

namespace Yis.Designer.Conceptual.Data.Memory
{
    public class ConceptualDataContext : DataContextBase, IConceptualDataContext
    {
        #region Constructors + Destructors

        public ConceptualDataContext()
            : base(@"D:\DataYis\Conceptual\")
        {
        }

        #endregion Constructors + Destructors
    }
}