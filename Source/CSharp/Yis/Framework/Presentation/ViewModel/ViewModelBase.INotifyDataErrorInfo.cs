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
        private Dictionary<string, List<ValidationResult>> _errors;
        private bool _hasErrors;
        #endregion

        #region prorpriétés
        private Dictionary<string, List<ValidationResult>> Errors
        {
            get
            {
                if (_errors == null)
                {
                    _errors = new Dictionary<string, List<ValidationResult>>();
                }
                return _errors;
            }
        }
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
                if (Errors.TryGetValue(propertyName, out result))
                {
                    return result;
                }
                return _noErrors;
            }
            else
            {
                return Errors.Values.SelectMany(x => x).Distinct().ToArray();
            }
        }


        private EventHandler<DataErrorsChangedEventArgs> _errorsChanged;

        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
        }

        private void RaiseErrorsChanged(string propertyName = "")
        {
            HasErrors = Errors.Any();
            var args = new DataErrorsChangedEventArgs(propertyName);
            OnErrorsChanged(args);
            if (_errorsChanged != null) { _errorsChanged(this, args); }
        }
        #endregion

        #region Implementation de INotifyDataErrorInfoGetErrors(propertyName).Select(e => e.ErrorMessage).ToArray()

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { _errorsChanged += value; }
            remove { _errorsChanged -= value; }
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {           
            return GetErrors(propertyName).Select(e => e.ErrorMessage);         
        }

        public bool HasErrors
        {
            get { return _hasErrors; }
            private set { SetValue<bool>(ref _hasErrors, value, false); }
        }

        #endregion
    }
}
