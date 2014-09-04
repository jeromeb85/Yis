using Yis.Framework.Core.Rule.Contract;

namespace Yis.Framework.Core.Rule
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