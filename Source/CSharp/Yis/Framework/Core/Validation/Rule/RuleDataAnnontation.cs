using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yis.Framework.Core.Validation.Contract;

namespace Yis.Framework.Core.Validation.Rule
{
    public class RuleDataAnnotation : IRule
    {
        #region Constructors + Destructors

        public RuleDataAnnotation(PropertyInfo prop, ValidationAttribute attribute)
        {
            Attribute = attribute;
            _prop = prop;
        }

        #endregion Constructors + Destructors

        #region Fields

        private PropertyInfo _prop;

        #endregion Fields

        #region Properties

        public ValidationAttribute Attribute { get; private set; }

        #endregion Properties

        #region Methods

        public IEnumerable<ValidationResult> Execute(IRuleContext context)
        {
            var ctx = new ValidationContext(context.GetTarget(), null, null);
            List<ValidationResult> list = new List<ValidationResult>();

            if (_prop != null)
                ctx.MemberName = _prop.Name;

            ValidationResult result = null;

            try
            {
                if (_prop != null)
                {
                    result = this.Attribute.GetValidationResult(_prop.GetValue(context.GetTarget()), ctx);
                }
                else
                {
                    result = this.Attribute.GetValidationResult(null, ctx);
                }
            }
            catch (Exception)
            {
            }

            if (result != null)
                list.Add(result);

            return list;
        }

        #endregion Methods
    }
}