using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Messaging.Contract;
using Yis.Framework.Core.Validation;
using Yis.Framework.Presentation.Navigation.Contract;

namespace Yis.Framework.Presentation.ViewModel
{
    public abstract partial class ViewModelBase : IViewModel
    {
        #region Fields

        private bool _isChanged;

        private INavigation _navigation;

        private RuleValidator _validator;

        private IServiceLocator _locator;

        #endregion Fields

        #region Constructors

        protected ViewModelBase()
        {
            AutoValidateProperty = true;
            _hasErrors = false;

            OnRuleInialize();
        }

        #endregion Constructors

        #region Properties

        public bool AutoValidateProperty { get; set; }

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

        protected IServiceLocator Locator
        {
            get
            {
                if (_locator == null)
                {
                    _locator = DependencyResolverManager.Default.Resolve<IServiceLocator>();
                }

                return _locator;
            }
        }

        protected INavigation Navigation
        {
            get
            {
                if (_navigation == null)
                {
                    _navigation = DependencyResolverManager.Default.Resolve<INavigation>();
                }

                return _navigation;
            }
        }

        private IBus _bus;

        protected IBus Bus
        {
            get
            {
                if (_bus == null)
                {
                    _bus = DependencyResolverManager.Default.Resolve<IBus>();
                }

                return _bus;
            }
        }

        #endregion Properties

        #region Methods

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

        protected virtual void OnRuleInialize()
        {
        }

        protected void SetValue<T>(ref T property, T newValue, [CallerMemberName] string name = null)
        {
            SetValue<T>(ref property, newValue, AutoValidateProperty, name);
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

        #endregion Methods
    }

    public abstract partial class ViewModelBase<TModel> : ViewModelBase
    {
        #region Constructors

        protected ViewModelBase(TModel model)
            : base()
        {
            Model = model;
        }

        #endregion Constructors

        #region Properties

        protected TModel Model { get; private set; }

        #endregion Properties
    }
}