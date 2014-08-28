using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Data;
using Yis.Designer.Model;
using Yis.Framework.Business;

namespace Yis.Designer.Business
{
    public class WorkSpaceManager : BusinessComponentBase<WorkSpace,IWorkSpaceProvider>
    {
        public WorkSpaceManager() : base ("YisDataContext")
        {
        }
    }
}
