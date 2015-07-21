using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Conceptual.Business
{
    public class AttributeCollection : BusinessObjectCollectionBase<AttributeCollection, Attribute, Model.Attribute, IAttributeProvider, IConceptualDataContext>
    {
        #region Constructors

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

        #endregion Constructors

        #region Methods

        public static AttributeCollection GetByConcept(Guid idConcept)
        {
            return new AttributeCollection(Provider.GetByConcept(idConcept));
        }

        #endregion Methods
    }
}