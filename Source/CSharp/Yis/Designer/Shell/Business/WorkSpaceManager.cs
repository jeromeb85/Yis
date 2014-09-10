using Yis.Designer.Data.Contract;
using Yis.Designer.Model;
using Yis.Framework.Business;

namespace Yis.Designer.Business
{
    public class WorkSpaceManager : BusinessComponentBase<WorkSpace, IWorkSpaceProvider, IDesignerDataContext>
    {
        public WorkSpaceManager()
            : base("YisDataContext")
        {
        }
    }
}