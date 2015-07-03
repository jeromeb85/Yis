using Yis.Framework.Core.Validation.Contract;

namespace Yis.Framework.Core.Validation
{
    public class RuleContext : IRuleContext
    {
        #region Constructors + Destructors

        public RuleContext(object target)
        {
            Target = target;
        }

        #endregion Constructors + Destructors

        #region Properties

        private object Target { get; set; }

        #endregion Properties

        #region Methods

        public object GetTarget()
        {
            return Target;
        }

        #endregion Methods
    }
}