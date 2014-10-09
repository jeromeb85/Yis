using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Business;
using Yis.Framework.Core.Extension;

namespace Yis.Designer.Conceptual.Business
{
    public class Concept : BusinessObjectBase<Concept, Model.Concept, IConceptProvider, IConceptualDataContext>
    {
        #region Constructors

        public Concept(Model.Concept model)
            : base(model)
        {
        }

        public Concept()
            : base()
        {
            Id = Guid.NewGuid();
        }

        #endregion Constructors

        #region Properties

        public AttributeCollection Attribute
        {
            get { return GetProperty<AttributeCollection>(() => AttributeCollection.GetByConcept(Id), OnLoadAttribute, IsChildAutoSave: true, IsChildAutoDelete: true); }
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

        public Domain Parent
        {
            get { return GetProperty<Domain>(() => Domain.GetById(Model.DomainId)); }
            set { SetProperty(v => Model.DomainId = value.Id, Model.DomainId, value.Id); }
        }

        #endregion Properties

        #region Methods

        public static Concept GetById(Guid id)
        {
            Model.Concept model = Provider.GetById(id);
            Concept item = null;

            if (!model.IsNull())
                item = new Concept(model);

            return item;
        }

        public static Concept GetByName(string name)
        {
            Model.Concept model = Provider.GetByName(name);
            Concept item = null;

            if (!model.IsNull())
                item = new Concept(model);

            return item;
        }

        private void OnAddedNewAttribute(object sender, AddedNewEventArgs<Business.Attribute> item)
        {
            item.NewObject.Parent = this;
        }

        private void OnLoadAttribute(AttributeCollection item)
        {
            item.AddedNew += OnAddedNewAttribute;
        }

        #endregion Methods
    }
}