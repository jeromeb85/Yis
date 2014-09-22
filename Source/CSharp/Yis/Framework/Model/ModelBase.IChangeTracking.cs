using System.ComponentModel;
using System.Xml.Serialization;

namespace Yis.Framework.Model
{
    public abstract partial class ModelBase : IChangeTracking
    {
        private bool _isChanged;

        public void AcceptChanges()
        {
            _cacheBackup = null;
            _isChanged = false;
        }

        [XmlIgnore]
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
                    _isChanged = value;
                }
            }
        }
    }
}