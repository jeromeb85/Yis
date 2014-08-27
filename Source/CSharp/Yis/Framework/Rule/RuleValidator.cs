using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Rule
{
    public class RuleValidator
    {
        private object _context;
        private Dictionary<string, List<IRule>> _ruleCache;
        private Dictionary<string, List<IRule>> RuleCache
        {
            get
            {
                if (_ruleCache == null)
                {
                    _ruleCache = new Dictionary<string, List<IRule>>();
                }
                return _ruleCache;
            }
        }

        public RuleValidator(object context)
        {
            _context = context;
        }

        public void AddRule(Expression<Func<object>> property, Func<bool> validateDelegate, string errorMessage)
        {
            AddRule(GetMemberName(property.Body), validateDelegate, errorMessage);
        }
        public void AddRule(string propertyName, Func<bool> validateDelegate, string errorMessage)
        {
            RegisterRule(propertyName, new RuleDelegate(validateDelegate, errorMessage));
        }

        public void AddRuleAnnotation(Type metadataType)
        {
            AddDataAnnotationsFromType(metadataType);
        }

        public IEnumerable<ValidationResult> Validate()
        {
            List<ValidationResult> list = new List<ValidationResult>();

            foreach (IRule item in RuleCache.Values)
            {
                list.AddRange(item.Execute(_context));
            }

            return list;
        }



        private void AddDataAnnotationsFromType(Type metadataType)
        {
            var attList = metadataType.GetCustomAttributes(typeof(ValidationAttribute), true);
            foreach (var att in attList)
                RegisterRule(null, new RuleDataAnnotation(null, (ValidationAttribute)att));
            
            var propList = metadataType.GetProperties();
            foreach (var prop in propList)
            {
                attList = prop.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.ValidationAttribute), true);
                foreach (var att in attList)
                {
                    RegisterRule(prop.Name, new RuleDataAnnotation(prop, (ValidationAttribute)att));
                }
            }
        }

        private void RegisterRule(string propertyName, IRule Rule)
        {
            if (RuleCache.ContainsKey(propertyName))
            {
                List<IRule> result;
                RuleCache.TryGetValue(propertyName, out result);
                result.Add(Rule);
            }
            else
            {
                RuleCache.Add(propertyName, new List<IRule>() { Rule });
            }
        }

        private static string GetMemberName(Expression expression, bool compound = true)
        {
            var memberExpression = expression as MemberExpression;

            if (memberExpression != null)
            {
                if (compound && memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    return GetMemberName(memberExpression.Expression) + "." + memberExpression.Member.Name;
                }

                return memberExpression.Member.Name;
            }

            var unaryExpression = expression as UnaryExpression;

            if (unaryExpression != null)
            {
                if (unaryExpression.NodeType != ExpressionType.Convert)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Cannot interpret member from {0}",
                                                                      expression));
                }

                return GetMemberName(unaryExpression.Operand);
            }

            throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Could not determine member from {0}",
                                                              expression));
        }

    }
}
