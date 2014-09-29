using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Software.Business
{
    public class NameSpace : BusinessObjectBase<NameSpace, Yis.Designer.Software.Model.NameSpace, INameSpaceProvider, ISoftwareDataContext>
    {
        #region Properties

        public string Name
        {
            get
            {
                return Model.Name;
            }

            set
            {
                SetProperty<string>(Model.Name, value);
            }
        }

        #endregion Properties
    }
}