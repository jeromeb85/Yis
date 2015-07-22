using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

namespace Yis.Designer.Conceptual.Model
{
    public enum AggregationType
    {
    }

    public enum RelationType
    { }

    public struct RelationshipLinkedPoint
    {
    }

    public class Relationship : ModelBase
    {
        #region Properties

        public Guid IdConceptPointDestination { get; set; }
        public Guid IdConceptPointSource { get; set; }

        //public Concept PointDestination { get; set; }

        //public RelationshipLinkedPoint PointInformationDestination { get; set; }

        //public RelationshipLinkedPoint PointInformationSource { get; set; }

        //public Concept PointSource { get; set; }

        #endregion Properties
    }
}