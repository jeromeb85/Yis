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
    public class Class : BusinessObjectBase<Class, Model.Class, IClassProvider, ISoftwareDataContext>
    {
        #region Constructors

        public Class(Model.Class model)
            : base(model)
        {
        }

        public Class()
            : base()
        {
            Id = Guid.NewGuid();
        }

        #endregion Constructors

        #region Properties

        public string BaseType
        {
            get { return GetProperty(() => Model.BaseType); }
            set { SetProperty(v => Model.BaseType = value, Model.BaseType, value); }
        }

        public Guid Id
        {
            get { return GetProperty(() => Model.Id); }
            set
            {
                if (!IsNew)
                    throw new Exception("Impossible d'affecter un Id si pas isNew");

                SetProperty(v => Model.Id = value, Model.Id, value);
            }
        }

        public IList<string> Implement
        {
            get { return GetProperty(() => Model.Implement.IsNull() ? Model.Implement = new List<string>() : Model.Implement); }
            //     set { SetProperty(v => Model.Import = String.Join(",", value), Model.Import, String.Join(",", value)); }
        }

        public IList<string> Import
        {
            get { return GetProperty(() => Model.Import.IsNull() ? Model.Import = new List<string>() : Model.Import); }
            //     set { SetProperty(v => Model.Import = String.Join(",", value), Model.Import, String.Join(",", value)); }
        }

        public string Name
        {
            get { return GetProperty(() => Model.Name); }
            set { SetProperty(v => Model.Name = value, Model.Name, value); }
        }

        public bool IsInterface
        {
            get { return GetProperty(() => Model.IsInterface); }
            set { SetProperty(v => Model.IsInterface = value, Model.IsInterface, value); }
        }

        public NameSpace Parent
        {
            get { return GetProperty<NameSpace>(() => NameSpace.GetById(Model.NameSpaceId)); }
            set { SetProperty(v => Model.NameSpaceId = value.Id, Model.NameSpaceId, value.Id); }
        }

        public PropertyCollection Property
        {
            get { return GetProperty<PropertyCollection>(() => PropertyCollection.GetByOwner(Id), OnLoadProperty, IsChildAutoSave: true, IsChildAutoDelete: true); }
        }

        public Model.Scope Scope
        {
            get { return GetProperty(() => Model.Scope); }
            set { SetProperty(v => Model.Scope = value, Model.Scope, value); }
        }

        #endregion Properties

        #region Methods

        public static Class GetById(Guid id)
        {
            Model.Class model = Provider.GetById(id);
            Class item = null;

            if (!model.IsNull())
                item = new Class(model);

            return item;
        }

        private void OnAddedNewProperty(object sender, AddedNewEventArgs<Property> item)
        {
            item.NewObject.Parent = this;
        }

        private void OnLoadProperty(PropertyCollection item)
        {
            item.AddedNew += OnAddedNewProperty;
        }

        #endregion Methods
    }
}