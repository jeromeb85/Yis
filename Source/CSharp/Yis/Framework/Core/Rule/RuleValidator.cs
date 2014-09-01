using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Rule.Contract;

namespace Yis.Framework.Core.Rule
{

    public class RuleValidator
    {
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

        protected void RegisterRule(string propertyName, IRule Rule)
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

        protected string GetMemberName(Expression expression, bool compound = true)
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

        public void AddRuleAnnotation(Type metadataType)
        {
            AddDataAnnotationsFromType(metadataType);
        }

        public IEnumerable<ValidationResult> Validate(IRuleContext context)
        {
            List<ValidationResult> result = new List<ValidationResult>();

            foreach (var dico in RuleCache)
            {
                List<IRule> list;
                RuleCache.TryGetValue(dico.Key, out list);

                foreach (IRule item in list)
                {
                    foreach (var error in item.Execute(context))
                    {
                        result.Add(new ValidationResult(error.ErrorMessage, new string[] { dico.Key }));
                    }

                }
            }

            return result;
        }

        public IEnumerable<ValidationResult> Validate(object target)
        {
            return Validate(new RuleContext(target));
        }

        public IEnumerable<ValidationResult> Validate(object target, string propertyName)
        {
            return Validate(new RuleContext(target), propertyName);
        }

        public IEnumerable<ValidationResult> Validate(IRuleContext context, string propertyName)
        {
            List<ValidationResult> result = new List<ValidationResult>();
            List<IRule> list;
            RuleCache.TryGetValue(propertyName, out list);

            if (list != null)
            {
                foreach (IRule item in list)
                {
                    foreach (var error in item.Execute(context))
                    {
                        result.Add(new ValidationResult(error.ErrorMessage, new string[] { propertyName }));
                    }
                }
            }


            return result;
        }

        public void AddRule<TTarget>(Expression<Func<TTarget, object>> property, Func<TTarget, bool> validateDelegate, string errorMessage)
        {
            AddRule(GetMemberName(property.Body), validateDelegate, errorMessage);
        }
        public void AddRule<TTarget>(string propertyName, Func<TTarget, bool> validateDelegate, string errorMessage)
        {
            RegisterRule(propertyName, new RuleDelegate<TTarget>(validateDelegate, errorMessage));
        }

        public void AddRuleAnnotation<TTarget>()
        {
            AddRuleAnnotation(typeof(TTarget));
        }

        public IEnumerable<ValidationResult> Validate<TTarget>(IRuleContext context, Expression<Func<TTarget, object>> property)
        {
            return Validate(context, GetMemberName(property.Body));
        }

        public IEnumerable<ValidationResult> Validate<TTarget>(object target, Expression<Func<TTarget, object>> property)
        {
            return Validate(new RuleContext(target), GetMemberName(property.Body));
        }

    }
}
