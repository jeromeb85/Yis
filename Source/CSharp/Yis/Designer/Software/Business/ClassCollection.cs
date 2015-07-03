using System;
using System.Collections.Generic;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Software.Business
{
    public class ClassCollection : BusinessObjectCollectionBase<ClassCollection, Class, Model.Class, IClassProvider, ISoftwareDataContext>
    {
        #region Constructors + Destructors

        public ClassCollection(ICollection<Class> list)
            : base(list)
        {
        }

        public ClassCollection(IEnumerable<Model.Class> list)
            : base(list)
        {
        }

        public ClassCollection()
            : base()
        {
        }

        #endregion Constructors + Destructors

        #region Methods

        public static ClassCollection GetByNameSpace(Guid idNameSpace)
        {
            return new ClassCollection(Provider.GetByNameSpace(idNameSpace));
        }

        #endregion Methods
    }
}