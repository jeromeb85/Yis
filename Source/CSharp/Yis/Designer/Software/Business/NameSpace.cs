using System;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Business;
using Yis.Framework.Core.Extension;

namespace Yis.Designer.Software.Business
{
    public class NameSpace : BusinessObjectBase<NameSpace, Model.NameSpace, INameSpaceProvider, ISoftwareDataContext>
    {
        #region Constructors + Destructors

        public NameSpace(Model.NameSpace model)
            : base(model)
        {
        }

        public NameSpace()
            : base()
        {
            Id = Guid.NewGuid();
        }

        #endregion Constructors + Destructors

        #region Properties

        public ClassCollection Class
        {
            get { return GetProperty<ClassCollection>(() => ClassCollection.GetByNameSpace(Id), OnLoadClass, IsChildAutoSave: true, IsChildAutoDelete: true); }
        }

        public string FullName
        {
            get { return ConstructFullName(this); }
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
            get { return GetProperty<NameSpace>(() => NameSpace.GetById(Model.ParentNameSpaceId)); }
            set { SetProperty(v => Model.ParentNameSpaceId = value.Id, Model.ParentNameSpaceId, value.Id); }
        }

        public NameSpaceCollection Sub
        {
            get { return GetProperty<NameSpaceCollection>(() => NameSpaceCollection.GetChildByParent(Id), OnLoadSub, IsChildAutoSave: true, IsChildAutoDelete: true); }
        }

        #endregion Properties

        #region Methods

        public static NameSpace GetById(Guid id)
        {
            Model.NameSpace model = Provider.GetById(id);
            NameSpace item = null;

            if (!model.IsNull())
                item = new NameSpace(model);

            return item;
        }

        public static NameSpace GetByName(string name)
        {
            Model.NameSpace model = Provider.GetByName(name);
            NameSpace item = null;

            if (!model.IsNull())
                item = new NameSpace(model);

            return item;
        }

        public static bool IsExists(string name)
        {
            return Provider.IsExists(name);
        }

        private string ConstructFullName(NameSpace item)
        {
            if (item.IsRoot)
                return item.Name;
            else
                return ConstructFullName(item.Parent) + "." + item.Name;
        }

        private void OnAddedNewClass(object sender, AddedNewEventArgs<Class> item)
        {
            item.NewObject.Parent = this;
        }

        private void OnAddedNewSub(object sender, AddedNewEventArgs<NameSpace> item)
        {
            item.NewObject.Parent = this;
        }

        private void OnLoadClass(ClassCollection item)
        {
            item.AddedNew += OnAddedNewClass;
        }

        private void OnLoadSub(NameSpaceCollection item)
        {
            item.AddedNew += OnAddedNewSub;
        }

        #endregion Methods
    }
}