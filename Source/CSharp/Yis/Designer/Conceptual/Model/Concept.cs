using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

namespace Yis.Designer.Conceptual.Model
{
    public class Concept : NamedElement
    {
        #region Properties

        public Guid DomainId { get; set; }

        #endregion Properties
    }
}