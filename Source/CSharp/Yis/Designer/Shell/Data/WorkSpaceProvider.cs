using Yis.Designer.Data.Contract;
using Yis.Designer.Model;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.EntityFramework;

namespace Yis.Designer.Data
{
    public class WorkSpaceProvider : RepositoryBase<WorkSpace>, IWorkSpaceProvider
    {
        #region Constructors + Destructors

        public WorkSpaceProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion Constructors + Destructors
    }
}