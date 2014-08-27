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
    public abstract partial class ModelBase : INotifyPropertyChanged

    {
        #region INotifyPropertyChanged Implementation
        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.IsChanged = true;
            //this.Backup();
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            if (propertyChanged != null) { propertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        }

        /// <summary>
        /// Occurs when the <see cref="E:PropertyChanged"/> event is raised.
        /// </summary>
        /// <param name="e">A <see cref="System.ComponentModel.PropertyChangedEventArgs"/> that contains the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {

        }

        private PropertyChangedEventHandler propertyChanged;
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChanged += value; }
            remove { propertyChanged -= value; }
        }
        #endregion

    }
}
