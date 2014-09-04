using System.ComponentModel;

namespace Yis.Framework.Model
{
    public abstract partial class ModelBase : IRevertibleChangeTracking
    {
        public void RejectChanges()
        {
            Restore();
            _cacheBackup = null;
            IsChanged = false;
        }
    }
}