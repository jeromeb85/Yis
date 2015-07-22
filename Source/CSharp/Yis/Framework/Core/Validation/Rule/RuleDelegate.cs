using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yis.Framework.Core.Validation.Contract;

namespace Yis.Framework.Core.Validation.Rule
{
    public class RuleDelegate<T> : IRule
    {
        private string _errorMessage;
        private Func<T, bool> _command;

        public RuleDelegate(Func<T, bool> command, string errorMessage)
        {
            _errorMessage = errorMessage;
            _command = command;
        }

        public IEnumerable<ValidationResult> Execute(IRuleContext context)
        {
            List<ValidationResult> list = new List<ValidationResult>();

            if (!_command((T)context.GetTarget()))
            {
                ValidationResult item = new ValidationResult(_errorMessage);
                list.Add(item);
            }

            return list;
        }
    }
}