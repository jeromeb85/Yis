using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Rule
{
    public class RuleContext : IRuleContext
    {
        private object Target { get; set; }

        public RuleContext(object target)
        {
            Target = target;
        }

        public object GetTarget()
        {
            return Target;
        }
    }
}
