using System.Collections.Generic;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Conceptual.Business
{
    public class DomainCollection : BusinessObjectCollectionBase<DomainCollection, Domain, Model.Domain, IDomainProvider, IConceptualDataContext>
    {
        #region Constructors + Destructors

        public DomainCollection(ICollection<Domain> list)
            : base(list)
        {
        }

        public DomainCollection(IEnumerable<Model.Domain> list)
            : base(list)
        {
        }

        public DomainCollection()
            : base()
        {
        }

        #endregion Constructors + Destructors
    }
}