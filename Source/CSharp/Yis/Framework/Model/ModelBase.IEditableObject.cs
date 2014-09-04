using System.ComponentModel;

namespace Yis.Framework.Model
{
    public abstract partial class ModelBase : IEditableObject
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

        #endregion Implementation de IEditableObject
    }
}