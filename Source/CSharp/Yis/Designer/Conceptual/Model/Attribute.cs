using System;

namespace Yis.Designer.Conceptual.Model
{
    public class Attribute : NamedElement
    {
        #region Properties

        public Guid ConceptId { get; set; }

        public string Type { get; set; }

        #endregion Properties
    }
}