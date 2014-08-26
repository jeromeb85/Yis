using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Model
{
    public partial class ModelBase : IEditableObject
    {

        #region Implementation de IEditableObject

        public void BeginEdit()
        {
            Backup();
        }

        public void CancelEdit()
        {
            RejectChanges();
        }
        public void EndEdit()
        {
            AcceptChanges();
        }
        #endregion
    }
}
