using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Yis.Framework.Presentation.ViewModel
{
    public abstract partial class ViewModelBase : INotifyPropertyChanging
    {
        #region Implementation de INotifyPropertyChanging

        public event PropertyChangingEventHandler PropertyChanging
        {
            add { _propertyChanging += value; }
            remove { _propertyChanging -= value; }
        }

        #endregion Implementation de INotifyPropertyChanging

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        protected void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            var args = new PropertyChangingEventArgs(propertyName);
            OnPropertyChanging(args);
            if (_propertyChanging != null) { _propertyChanging(this, args); }
        }

        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
        }

        private PropertyChangingEventHandler _propertyChanging;
    }
}