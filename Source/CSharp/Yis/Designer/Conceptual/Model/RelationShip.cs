using System;
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

        #endregion Properties

        //public Concept PointDestination { get; set; }

        //public RelationshipLinkedPoint PointInformationDestination { get; set; }

        //public RelationshipLinkedPoint PointInformationSource { get; set; }

        //public Concept PointSource { get; set; }
    }
}