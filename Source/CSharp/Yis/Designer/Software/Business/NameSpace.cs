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
    public class NameSpace : BusinessObjectBase<NameSpace, Model.NameSpace, INameSpaceProvider, ISoftwareDataContext>
    {
        #region Constructors

        public NameSpace(Model.NameSpace model)
            : base(model)
        {
        }

        public NameSpace()
            : base()
        {
        }

        #endregion Constructors

        #region Properties

        public Guid Id
        {
            get { return Model.Id; }
            set
            {
                if (!IsNew)
                    throw new Exception("Impossible d'affecter un Id si pas isNew");

                Model.Id = value;
            }
        }

        public bool IsRoot
        {
            get { return Parent.IsNull(); }
        }

        public string Name
        {
            get { return GetProperty(() => Model.Name); }
            set { SetProperty(v => Model.Name = value, Model.Name, value); }
        }

        public NameSpace Parent
        {
            get { return new NameSpace(Provider.GetById(Model.ParentNameSpaceId)); }
            set { SetProperty(v => Model.ParentNameSpaceId = value.Id, Model.ParentNameSpaceId, value.Id); }
        }

        public NameSpaceCollection Sub
        {
            get { return GetProperty<NameSpaceCollection>(() => NameSpaceCollection.GetChildByParent(Id), true, true); }
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

        public static bool IsExists(string name)
        {
            return Provider.IsExists(name);
        }

        #endregion Methods
    }
}