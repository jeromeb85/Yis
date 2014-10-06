using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Software.Business
{
    public class ClassCollection : BusinessObjectCollectionBase<ClassCollection, Class, Model.Class, IClassProvider, ISoftwareDataContext>
    {
        #region Constructors

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

        #endregion Constructors

        #region Methods

        public static ClassCollection GetByNameSpace(Guid idNameSpace)
        {
            return new ClassCollection(Provider.GetByNameSpace(idNameSpace));
        }

        #endregion Methods
    }
}