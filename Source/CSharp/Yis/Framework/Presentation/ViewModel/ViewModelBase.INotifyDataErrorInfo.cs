using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Presentation.ViewModel
{
    public abstract partial class ViewModelBase : INotifyDataErrorInfo
    {

        #region variables privées
        private static readonly ValidationResult[] _noErrors = new ValidationResult[0];
        private readonly Dictionary<string, List<ValidationResult>> _errors;
        private bool _hasErrors;
        #endregion

        #region méthodes
        public IEnumerable<ValidationResult> GetErrors()
        {
            return GetErrors(null);
        }

        public IEnumerable<ValidationResult> GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                List<ValidationResult> result;
                if (_errors.TryGetValue(propertyName, out result))
                {
                    return result;
                }
                return _noErrors;
            }
            else
            {
                return _errors.Values.SelectMany(x => x).Distinct().ToArray();
            }
        }


        private EventHandler<DataErrorsChangedEventArgs> _errorsChanged;

        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
        }

        private void RaiseErrorsChanged(string propertyName = "")
        {
            HasErrors = _errors.Any();
            var args = new DataErrorsChangedEventArgs(propertyName);
            OnErrorsChanged(args);
            if (_errorsChanged != null) { _errorsChanged(this, args); }
        }
        #endregion

        #region Implementation de INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { _errorsChanged += value; }
            remove { _errorsChanged -= value; }
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return GetErrors(propertyName);
        }

        public bool HasErrors
        {
            get { return _hasErrors; }
            private set { SetValue<bool>(ref _hasErrors, value); }
        }

        #endregion
    }
}
