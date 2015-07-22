using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

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