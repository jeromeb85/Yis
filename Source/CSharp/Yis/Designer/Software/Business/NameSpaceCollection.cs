using System;
using System.Collections.Generic;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Software.Business
{
    public class NameSpaceCollection : BusinessObjectCollectionBase<NameSpaceCollection, NameSpace, Model.NameSpace, INameSpaceProvider, ISoftwareDataContext>
    {
        #region Constructors + Destructors

        public NameSpaceCollection(ICollection<NameSpace> list)
            : base(list)
        {
        }

        public NameSpaceCollection(IEnumerable<Model.NameSpace> list)
            : base(list)
        {
        }

        public NameSpaceCollection()
            : base()
        {
        }

        #endregion Constructors + Destructors

        #region Methods

        public static NameSpaceCollection GetChildByParent(Guid parentId)
        {
            return new NameSpaceCollection(Provider.GetChildByParent(parentId));
        }

        #endregion Methods
    }
}