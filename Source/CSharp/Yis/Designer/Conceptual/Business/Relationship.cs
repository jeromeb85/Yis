using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Conceptual.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Conceptual.Business
{
    public class Relationship : BusinessObjectBase<Relationship, Model.Relationship, IRelationshipProvider, IConceptualDataContext>
    {

        public Concept PointSource
        {
            get { return GetProperty<Concept>(() => Concept.GetById(Model.IdConceptPointSource)); }
            set { SetProperty(v => Model.IdConceptPointSource = value.Id, Model.IdConceptPointSource, value.Id); }
        }

        public Concept PointDestination
        {
            get { return GetProperty<Concept>(() => Concept.GetById(Model.IdConceptPointDestination)); }
            set { SetProperty(v => Model.IdConceptPointDestination = value.Id, Model.IdConceptPointDestination, value.Id); }
        }
    }
}
