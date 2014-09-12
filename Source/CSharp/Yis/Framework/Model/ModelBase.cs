using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Yis.Framework.Model.Contract;

namespace Yis.Framework.Model
{
    public abstract partial class ModelBase : IModel
    {
        #region Fields

        private Dictionary<string, object> _cacheBackup;

        #endregion Fields

        #region Methods

        protected void SetValue<T>(ref T value, T newValue, [CallerMemberName] string name = null)
        {
            if (newValue != null)
            {
                if (!newValue.Equals(value))
                {
                    value = newValue;
                }
            }
            else
            {
                value = default(T);
            }
        }

        private void Backup()
        {
            _cacheBackup = new Dictionary<string, object>();
            IEnumerable<PropertyInfo> properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite);
            foreach (PropertyInfo property in properties)
                _cacheBackup.Add(property.Name, property.GetValue(this, null));
        }

        private void Restore()
        {
            if (_cacheBackup != null)
            {
                IEnumerable<PropertyInfo> properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite);
                foreach (PropertyInfo property in properties)
                    property.SetValue(this, _cacheBackup[property.Name], null);
            }
        }

        #endregion Methods
    }

    public class ModelBase<TKey> : ModelBase, IModel<TKey>
    {
        #region Fields

        private TKey _id;

        #endregion Fields

        #region Properties

        [Key]
        [Column("Id")]
        public TKey Id { get { return _id; } set { SetValue<TKey>(ref _id, value); } }

        #endregion Properties

        //[Column("CreatedDate")]
        //public DateTime DateCreated { get; set; }
        //[Column("LastModifiedDate")]
        //public DateTime DateLastModified { get; set; }
    }
}