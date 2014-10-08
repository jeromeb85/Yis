using Yis.Framework.Data.Memory;

namespace Yis.Designer.Conceptual.Data.Memory
{
    public class ConceptualDataContext : DataContextBase, ConceptualDataContext
    {
        #region Constructors

        public ConceptualDataContext()
            : base(@"D:\DataYis\Conceptual\")
        {
        }

        #endregion Constructors
    }
}