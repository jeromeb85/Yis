using System;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Business;
using Yis.Framework.Core.Extension;

namespace Yis.Designer.Conceptual.Business
{
    public class Attribute : BusinessObjectBase<Attribute, Model.Attribute, IAttributeProvider, IConceptualDataContext>
    {
        #region Constructors + Destructors

        public Attribute(Model.Attribute model)
            : base(model)
        {
        }

        public Attribute()
            : base()
        {
            Id = Guid.NewGuid();
        }

        #endregion Constructors + Destructors

        #region Properties

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

        public Concept Parent
        {
            get { return GetProperty<Concept>(() => Concept.GetById(Model.ConceptId)); }
            set { SetProperty(v => Model.ConceptId = value.Id, Model.ConceptId, value.Id); }
        }

        public string Type
        {
            get { return GetProperty(() => Model.Type); }
            set { SetProperty(v => Model.Type = value, Model.Type, value); }
        }

        #endregion Properties

        #region Methods

        public static Attribute GetById(Guid id)
        {
            Model.Attribute model = Provider.GetById(id);
            Attribute item = null;

            if (!model.IsNull())
                item = new Attribute(model);

            return item;
        }

        public static Attribute GetByName(string name)
        {
            Model.Attribute model = Provider.GetByName(name);
            Attribute item = null;

            if (!model.IsNull())
                item = new Attribute(model);

            return item;
        }

        #endregion Methods
    }
}