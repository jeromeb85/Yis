using Yis.Designer.Software.Data.Contract;
using Yis.Designer.Software.Model;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.PersistentMemory;

namespace Yis.Designer.Software.Data.MemoryPersistent
{
    public class NameSpaceProvider : RepositoryBase<NameSpace>, INameSpaceProvider
    {
        #region Constructors

        public NameSpaceProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors
    }
}