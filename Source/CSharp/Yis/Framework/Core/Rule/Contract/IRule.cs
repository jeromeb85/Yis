using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Core.Rule.Contract
{
    public interface IRule
    {
        IEnumerable<ValidationResult> Execute(IRuleContext context);
    }
}
