using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Business;
using Yis.Framework.Core.Extension;

namespace Yis.Designer.Software.Business
{
    public class NameSpace : BusinessObjectBase<NameSpace, Yis.Designer.Software.Model.NameSpace, Guid, INameSpaceProvider, ISoftwareDataContext>
    {
        #region Constructors

        public NameSpace(Yis.Designer.Software.Model.NameSpace model)
            : base(model)
        {
        }

        public NameSpace()
            : base()
        {
        }

        #endregion Constructors

        #region Properties

        public NameSpaceCollection Child
        {
            get { return GetProperty<NameSpaceCollection>(() => NameSpaceCollection.GetByParent(Id), true); }
        }

        public string Name
        {
            get { return GetProperty(() => Model.Name); }
            set { SetProperty(v => Model.Name = value, Model.Name, value); }
        }

        #endregion Properties

        #region Methods

        public static NameSpace GetByName(string name)
        {
            Yis.Designer.Software.Model.NameSpace model = Provider.GetByName(name);
            NameSpace item = null;

            if (!model.IsNull())
                item = new NameSpace(model);

            return item;
        }

        public static bool IsExsist(string name)
        {
            return Provider.IsExists(name);
        }

        #endregion Methods
    }
}