using Yis.Framework.Core.Validation.Contract;

namespace Yis.Framework.Core.Validation
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