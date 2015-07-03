using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Yis.Framework.Presentation.ViewModel
{
    public abstract partial class ViewModelBase : INotifyPropertyChanging
    {
        #region Fields

        private PropertyChangingEventHandler _propertyChanging;

        #endregion Fields

        #region Events

        public event PropertyChangingEventHandler PropertyChanging
        {
            add { _propertyChanging += value; }
            remove { _propertyChanging -= value; }
        }

        #endregion Events

        #region Methods

        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        protected void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            var args = new PropertyChangingEventArgs(propertyName);
            OnPropertyChanging(args);
            if (_propertyChanging != null) { _propertyChanging(this, args); }
        }

        #endregion Methods
    }
}