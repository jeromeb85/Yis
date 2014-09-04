using System;
using Yis.Designer.Model;
using Yis.Framework.Data.Contract;
using Yis.Framework.Data.EntityFramework;

namespace Yis.Designer.Data
{
    public class WorkSpaceProvider : RepositoryBase<WorkSpace, Guid>, IWorkSpaceProvider
    {
        public WorkSpaceProvider(IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}