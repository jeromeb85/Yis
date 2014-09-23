using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using YAXLib;

namespace Yis.Framework.Model
{
    public abstract partial class ModelBase : IChangeTracking
    {
        #region Fields

        private bool _isChanged;

        #endregion Fields

        #region Properties

        [XmlIgnore]
        [YAXDontSerialize]
        [IgnoreDataMember]
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

        #endregion Properties

        #region Methods

        public void AcceptChanges()
        {
            _cacheBackup = null;
            _isChanged = false;
        }

        #endregion Methods
    }
}