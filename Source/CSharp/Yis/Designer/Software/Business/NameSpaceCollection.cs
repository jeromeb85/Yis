using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Software.Business
{
    public class NameSpaceCollection : BusinessObjectCollectionBase<NameSpaceCollection, NameSpace, Yis.Designer.Software.Model.NameSpace, INameSpaceProvider, ISoftwareDataContext>
    {
        #region Constructors

        public NameSpaceCollection(ICollection<NameSpace> list)
            : base(list)
        {
        }

        #endregion Constructors
    }
}