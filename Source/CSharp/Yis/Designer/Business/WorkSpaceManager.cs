using Yis.Designer.Model;
using Yis.Framework.Business;

namespace Yis.Designer.Business
{
    public class WorkSpaceManager : BusinessComponentBase<WorkSpace, IWorkSpaceProvider>
    {
        public WorkSpaceManager()
            : base("YisDataContext")
        {
        }
    }
}