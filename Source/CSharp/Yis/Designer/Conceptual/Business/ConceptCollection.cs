using System;
using System.Collections.Generic;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Conceptual.Business
{
    public class ConceptCollection : BusinessObjectCollectionBase<ConceptCollection, Concept, Model.Concept, IConceptProvider, IConceptualDataContext>
    {
        #region Constructors + Destructors

        public ConceptCollection(ICollection<Concept> list)
            : base(list)
        {
        }

        public ConceptCollection(IEnumerable<Model.Concept> list)
            : base(list)
        {
        }

        public ConceptCollection()
            : base()
        {
        }

        #endregion Constructors + Destructors

        #region Methods

        public static ConceptCollection GetByDomain(Guid idDomain)
        {
            return new ConceptCollection(Provider.GetByDomain(idDomain));
        }

        #endregion Methods
    }
}