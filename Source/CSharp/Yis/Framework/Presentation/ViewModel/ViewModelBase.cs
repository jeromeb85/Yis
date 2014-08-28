using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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


        #endregion

        #region Constructeurs
        protected ViewModelBase()
        {
            AutoValidateProperty = true;
            _errors = new Dictionary<string, List<ValidationResult>>();
            _hasErrors = false;

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
                        ValidateProperty(newValue, name);
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
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
            if (validationResults.Any())
            {
                _errors.Clear();
                foreach (var validationResult in validationResults)
                {
                    var propertyNames = validationResult.MemberNames.Any() ? validationResult.MemberNames : new string[] { "" };
                    foreach (string propertyName in propertyNames)
                    {
                        if (!_errors.ContainsKey(propertyName))
                        {
                            _errors.Add(propertyName, new List<ValidationResult>() { validationResult });
                        }
                        else
                        {
                            _errors[propertyName].Add(validationResult);
   
                        }
                        RaiseErrorsChanged(propertyName);
                    }
                }
                return false;
            }
            else
            {
                if (_errors.Any())
                {
                    _errors.Clear();
                    RaiseErrorsChanged();
                }
            }
            return true;
        }

        protected bool ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) { throw new ArgumentException("The argument propertyName must not be null or empty."); }

            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, validationResults);
            if (validationResults.Any())
            {
                _errors[propertyName] = validationResults;
                RaiseErrorsChanged(propertyName);
                return false;
            }
            else
            {
                if (_errors.Remove(propertyName))
                {
                    RaiseErrorsChanged(propertyName);
                }
            }
            return true;
        }
    }
}
