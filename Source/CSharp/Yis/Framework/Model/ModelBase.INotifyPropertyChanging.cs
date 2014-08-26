using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Model
{
    public partial class ModelBase : INotifyPropertyChanging
    {
        #region INotifyPropertyChanging Implementation
        /// <summary>
        /// Raises the <see cref="E:PropertyChanging"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that is changing.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        protected void RaisePropertyChanging([CallerMemberName] string propertyName = "")
        {
            if (!this.IsChanged) this.Backup();
            var args = new PropertyChangingEventArgs(propertyName);
            OnPropertyChanging(args);
            if (_propertyChanging != null) { _propertyChanging(this, args); }
        }

        /// <summary>
        /// Occurs when the <see cref="E:PropertyChanging"/> event is raised.
        /// </summary>
        /// <param name="e">A <see cref="System.ComponentModel.PropertyChangingEventArgs"/> that contains the event data.</param>
        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {

        }

        private PropertyChangingEventHandler _propertyChanging;
        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging
        {
            add { _propertyChanging += value; }
            remove { _propertyChanging -= value; }
        }
        #endregion

    }
}
