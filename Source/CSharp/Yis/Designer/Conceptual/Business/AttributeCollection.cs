using System;
using System.Collections.Generic;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Conceptual.Business
{
    public class AttributeCollection : BusinessObjectCollectionBase<AttributeCollection, Attribute, Model.Attribute, IAttributeProvider, IConceptualDataContext>
    {
        #region Constructors + Destructors

        public AttributeCollection(ICollection<Attribute> list)
            : base(list)
        {
        }

        public AttributeCollection(IEnumerable<Model.Attribute> list)
            : base(list)
        {
        }

        public AttributeCollection()
            : base()
        {
        }

        #endregion Constructors + Destructors

        #region Methods

        public static AttributeCollection GetByConcept(Guid idConcept)
        {
            return new AttributeCollection(Provider.GetByConcept(idConcept));
        }

        #endregion Methods
    }
}