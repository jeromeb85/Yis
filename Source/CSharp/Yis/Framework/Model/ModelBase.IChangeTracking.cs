using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Model
{
    public partial class ModelBase : IChangeTracking
    {
        private bool _isChanged;

        public void AcceptChanges()
        {            
            _cacheBackup = null;
            _isChanged = false;
        }

        public bool IsChanged
        {
            get
            {
                return _isChanged;
            }
            protected set
            {
                if (_isChanged != value)
                {
                    RaisePropertyChanging("IsChanged");
                    _isChanged = value;
                    RaisePropertyChanged("IsChanged");
                }
            }

        }
    }
}
