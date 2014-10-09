using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

namespace Yis.Designer.Conceptual.Model
{
    public class Attribute : ModelBase
    {
        #region Properties

        public Guid ConceptId { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        #endregion Properties
    }
}