using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Model
{
    public partial class ModelBase : INotifyDataErrorInfo
    {
        #region INotifyDataErrorInfo Implementation
        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return !this.GetErrorsInternal(null).Count.Equals(0);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanging"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property on which errors have changed.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        protected void RaiseErrorsChanged(string propertyName = "")
        {
            var args = new DataErrorsChangedEventArgs(propertyName);
            OnErrorsChanged(args);
            if (_errorsChanged != null) { _errorsChanged(this, args); }
        }

        /// <summary>
        /// Occurs when the <see cref="E:ErrorsChanged"/> event is raised.
        /// </summary>
        /// <param name="e">A <see cref="System.ComponentModel.DataErrorsChangedEventArgs"/> that contains the event data.</param>
        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {

        }

        private EventHandler<DataErrorsChangedEventArgs> _errorsChanged;
        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { _errorsChanged += value; }
            remove { _errorsChanged -= value; }
        }

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or null or System.String.Empty, to retrieve entity-level errors.<see cref="System.String.Empty"/>, pour récupérer les erreurs de l'entité.</param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return this.GetErrorsInternal(propertyName);
        }

        private List<ValidationResult> GetErrorsInternal(string propertyName)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(propertyName))
            {
                ValidationContext context = new ValidationContext(this, null, null);
                Validator.TryValidateObject(this, context, errors);
            }
            else
            {
                object value = TypeDescriptor.GetProperties(this)[propertyName].GetValue(this);

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                Validator.TryValidateProperty(value, context, errors);
            }

            return errors;
        }
        #endregion

        public bool Validate()
        {
            //List<ValidationResult> validationResults = new List<ValidationResult>();
            //Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
            //if (validationResults.Any())
            //{
            //    errors.Clear();
            //    foreach (var validationResult in validationResults)
            //    {
            //        var propertyNames = validationResult.MemberNames.Any() ? validationResult.MemberNames : new string[] { "" };
            //        foreach (string propertyName in propertyNames)
            //        {
            //            if (!errors.ContainsKey(propertyName))
            //            {
            //                errors.Add(propertyName, new List<ValidationResult>() { validationResult });
            //            }
            //            else
            //            {
            //                errors[propertyName].Add(validationResult);
            //            }
            //        }
            //    }
            //    RaiseErrorsChanged();
            //    return false;
            //}
            //else
            //{
            //    if (errors.Any())
            //    {
            //        errors.Clear();
            //        RaiseErrorsChanged();
            //    }
            //}
            return true;
        }

         protected bool SetPropertyAndValidate<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            //if (string.IsNullOrEmpty(propertyName)) { throw new ArgumentException("The argument propertyName must not be null or empty."); }

            //if (SetProperty(ref field, value, propertyName))
            //{
            //    ValidateProperty(value, propertyName);
            //    return true;
            //}
            return false;
        }

        protected bool ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            //if (string.IsNullOrEmpty(propertyName)) { throw new ArgumentException("The argument propertyName must not be null or empty."); }

            //List<ValidationResult> validationResults = new List<ValidationResult>();
            //Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, validationResults);
            //if (validationResults.Any())
            //{
            //    errors[propertyName] = validationResults;
            //    RaiseErrorsChanged(propertyName);
            //    return false;
            //}
            //else
            //{
            //    if (errors.Remove(propertyName))
            //    {
            //        RaiseErrorsChanged(propertyName);
            //    }
            //}
            return true;
        }
    }

}
