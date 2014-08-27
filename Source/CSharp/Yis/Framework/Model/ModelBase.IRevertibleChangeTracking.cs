using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Model
{
    public  abstract partial class ModelBase : IRevertibleChangeTracking

    {
        public void RejectChanges()
        {
            Restore();
            _cacheBackup = null;
            IsChanged = false;

        }
    }
}
