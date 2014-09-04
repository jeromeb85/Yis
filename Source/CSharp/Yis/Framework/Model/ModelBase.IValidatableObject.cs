using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yis.Framework.Model
{
    public abstract partial class ModelBase : IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}