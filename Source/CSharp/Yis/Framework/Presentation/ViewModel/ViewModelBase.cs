using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Rule;

namespace Yis.Framework.Presentation.ViewModel
{
    public  abstract partial class ViewModelBase : IViewModel
    {
        #region Propriétés
        private bool _isChanged;
        public bool IsChanged
        {
            get
            {
                return _isChanged;
            }
            protected set
            {
                SetValue<bool>(ref _isChanged, value, false);
            }

        }

        public bool AutoValidateProperty { get; set; }


        private RuleValidator _validator;
        public RuleValidator Validator
        {
            get
            {
                if (_validator == null)
                {
                    _validator = new RuleValidator();
                    _validator.AddRuleAnnotation(this.GetType());
                }

                return _validator;
            }
        }

        #endregion

        #region Constructeurs
        protected ViewModelBase()
        {
            AutoValidateProperty = true;            
            _hasErrors = false;

            OnRuleInialize();

        }
        #endregion

        protected void SetValue<T>(ref T property, T newValue, [CallerMemberName] string name = null)
        {
            SetValue<T>(ref property, newValue, AutoValidateProperty, name);
        }

        private void SetValue<T>(ref T property, T newValue, bool validate, [CallerMemberName] string name = null)
        {
            if (newValue != null)
            {
                if (!newValue.Equals(property))
                {
                    RaisePropertyChanging(name);
                    property = newValue;
                    RaisePropertyChanged(name);

                    if (validate)
                    {
                        Validate(name);
                    }
                }
            }
            else
            {
                property = default(T);
            }
        }

        public bool Validate()
        {

            IEnumerable<ValidationResult> validationResults = Validator.Validate(this); 
            //Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
            if (validationResults.Any())
            {
                Errors.Clear();
                foreach (var validationResult in validationResults)
                {
                    var propertyNames = validationResult.MemberNames.Any() ? validationResult.MemberNames : new string[] { "" };
                    foreach (string propertyName in propertyNames)
                    {
                        if (!Errors.ContainsKey(propertyName))
                        {
                            Errors.Add(propertyName, new List<ValidationResult>() { validationResult });
                        }
                        else
                        {
                            Errors[propertyName].Add(validationResult);
   
                        }
                        RaiseErrorsChanged(propertyName);
                    }
                }
                return false;
            }
            else
            {
                if (Errors.Any())
                {
                    Errors.Clear();
                    RaiseErrorsChanged();
                }
            }
            return true;
        }

        protected bool Validate([CallerMemberName] string propertyName = null)
        {
            IEnumerable<ValidationResult> validationResults = Validator.Validate(this, propertyName);
            //Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
            if (validationResults.Any())
            {
                Errors.Remove(propertyName);

                foreach (var validationResult in validationResults)
                {
                    var propertyNames = validationResult.MemberNames.Any() ? validationResult.MemberNames : new string[] { "" };
                    foreach (string item in propertyNames)
                    {
                        if (!Errors.ContainsKey(item))
                        {
                            Errors.Add(item, new List<ValidationResult>() { validationResult });
                        }
                        else
                        {
                            Errors[item].Add(validationResult);

                        }
                        
                    }

                    
                }

                RaiseErrorsChanged(propertyName);
                return false;
            }
            else
            {
                if (Errors.Remove(propertyName))
                {                    
                    RaiseErrorsChanged(propertyName);
                }
            }
            return true;

            //    if (string.IsNullOrEmpty(propertyName)) { throw new ArgumentException("The argument propertyName must not be null or empty."); }

            //    IEnumerable<ValidationResult> validationResults = Validator.Validate(this, propertyName);
            //    //Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, validationResults);
            //    if (validationResults.Any())
            //    {
            //        Errors[propertyName] = validationResults.ToList();
            //        RaiseErrorsChanged(propertyName);
            //        return false;
            //    }
            //    else
            //    {
            //        if (Errors.Remove(propertyName))
            //        {
            //            RaiseErrorsChanged(propertyName);
            //        }
            //    }
            //    return true;
            //}
        }
        protected virtual void OnRuleInialize()
        {

        }
    }
}
