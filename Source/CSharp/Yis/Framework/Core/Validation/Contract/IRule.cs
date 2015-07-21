using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yis.Framework.Core.Validation.Contract
{
    public interface IRule
    {
        IEnumerable<ValidationResult> Execute(IRuleContext context);
    }
}