using System;
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
    /// <summary>
    /// Classe de base pour les ViewModel
    /// </summary>
    public abstract partial class ViewModelBase : IViewModel
    {
        #region Constructors + Destructors

        /// <summary>
        /// Constructeurs
        /// </summary>
        protected ViewModelBase()
        {
            AutoValidateProperty = true;
            _hasErrors = false;

            OnRuleInialize();
        }

        #endregion Constructors + Destructors

        #region Fields

        private IBus _bus;
        private bool _isChanged;

        private IServiceLocator _locator;
        private INavigation _navigation;

        private RuleValidator _validator;

        #endregion Fields

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
                SetProperty<bool>(ref _isChanged, value, false);
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

        #endregion Properties

        #region Methods

        /// <summary>
        /// Lance la validation de l'objet
        /// </summary>
        /// <returns>Retourne True si l'objet répond aux contraintes de validation</returns>
        public bool Validate()
        {
            IEnumerable<ValidationResult> validationResults = Validator.Validate(this);
            //Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
            if (validationResults.Any())
            {
                foreach (var error in Errors)
                {
                    error.Value.Clear();
                    RaiseErrorsChanged(error.Key);
                }

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

        protected void SetProperty<T>(ref T property, T newValue, [CallerMemberName] string name = null)
        {
            SetProperty<T>(ref property, newValue, AutoValidateProperty, name);
        }

        protected void SetProperty<T>(Action<T> setAction, T oldValue, T newValue, [CallerMemberName] string name = null)
        {
            SetProperty<T>(setAction, oldValue, newValue, AutoValidateProperty, name);
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

        private void SetProperty<T>(ref T property, T newValue, bool validate, [CallerMemberName] string name = null)
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

        private void SetProperty<T>(Action<T> setAction, T oldValue, T newValue, bool validate, [CallerMemberName] string name = null)
        {
            if (newValue != null)
            {
                if (!newValue.Equals(oldValue))
                {
                    RaisePropertyChanging(name);
                    setAction(newValue);
                    RaisePropertyChanged(name);

                    if (validate)
                    {
                        Validate(name);
                    }
                }
            }
            else
            {
                setAction(default(T));
            }
        }

        #endregion Methods
    }

    public abstract partial class ViewModelBase<TModel> : ViewModelBase
    {
        #region Constructors + Destructors

        protected ViewModelBase(TModel model)
            : base()
        {
            Model = model;
        }

        #endregion Constructors + Destructors

        #region Properties

        protected TModel Model { get; private set; }

        #endregion Properties
    }
}