using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Designer.Conceptual.Model
{
    public abstract class NamedElement : Element
    {
        public string Name { get; set; }
    }
}
