using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Conceptual.Business
{
    public class ConceptCollection : BusinessObjectCollectionBase<ConceptCollection, Concept, Model.Concept, IConceptProvider, IConceptualDataContext>
    {
        #region Constructors

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

        #endregion Constructors

        #region Methods

        public static ConceptCollection GetByDomain(Guid idDomain)
        {
            return new ConceptCollection(Provider.GetByDomain(idDomain));
        }

        #endregion Methods
    }
}