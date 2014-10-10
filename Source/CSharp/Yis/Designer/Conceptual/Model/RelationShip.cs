using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class Relationship : Element
    {
        #region Properties

        public Concept PointDestination { get; set; }

        public RelationshipLinkedPoint PointInformationDestination { get; set; }

        public RelationshipLinkedPoint PointInformationSource { get; set; }

        public Concept PointSource { get; set; }

        #endregion Properties
    }
}