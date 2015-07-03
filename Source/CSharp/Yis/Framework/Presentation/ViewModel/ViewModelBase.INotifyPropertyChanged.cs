using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Yis.Framework.Presentation.ViewModel
{
    public abstract partial class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields

        private PropertyChangedEventHandler _propertyChanged;

        #endregion Fields

        #region Events

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }

        #endregion Events

        #region Methods

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var args = new PropertyChangedEventArgs(propertyName);
            this.IsChanged = true;
            OnPropertyChanged(args);
            if (_propertyChanged != null) { _propertyChanged(this, args); }
        }

        #endregion Methods
    }
}