using System;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Business;
using Yis.Framework.Core.Extension;

namespace Yis.Designer.Conceptual.Business
{
    public class Domain : BusinessObjectBase<Domain, Model.Domain, IDomainProvider, IConceptualDataContext>
    {
        #region Constructors + Destructors

        public Domain(Model.Domain model)
            : base(model)
        {
        }

        public Domain()
            : base()
        {
            Id = Guid.NewGuid();
        }

        #endregion Constructors + Destructors

        #region Properties

        public ConceptCollection Concept
        {
            get { return GetProperty<ConceptCollection>(() => ConceptCollection.GetByDomain(Id), OnLoadConcept, IsChildAutoSave: true, IsChildAutoDelete: true); }
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

        public string Name
        {
            get { return GetProperty(() => Model.Name); }
            set { SetProperty(v => Model.Name = value, Model.Name, value); }
        }

        #endregion Properties

        #region Methods

        public static Domain GetById(Guid id)
        {
            Model.Domain model = Provider.GetById(id);
            Domain item = null;

            if (!model.IsNull())
                item = new Domain(model);

            return item;
        }

        public static Domain GetByName(string name)
        {
            Model.Domain model = Provider.GetByName(name);
            Domain item = null;

            if (!model.IsNull())
                item = new Domain(model);

            return item;
        }

        public static bool IsExists(string name)
        {
            return Provider.IsExists(name);
        }

        private void OnAddedNewConcept(object sender, AddedNewEventArgs<Concept> item)
        {
            item.NewObject.Parent = this;
        }

        private void OnLoadConcept(ConceptCollection item)
        {
            item.AddedNew += OnAddedNewConcept;
        }

        #endregion Methods
    }
}