using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Rule
{
    public class RuleDelegate : IRule
    {
        private string _errorMessage;
        private Func<bool> _command;

        public RuleDelegate(Func<bool> command, string errorMessage)
        {            
            _errorMessage = errorMessage;
            _command = command;
        }

        public IEnumerable<ValidationResult> Execute(object context)
        {
            List<ValidationResult> list = new List<ValidationResult>();

            if (!_command())
            {
                ValidationResult item = new ValidationResult(_errorMessage);
                list.Add(item);
            }

            return list;
        }
    }
}
