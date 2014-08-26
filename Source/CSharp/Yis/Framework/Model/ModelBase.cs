using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Model
{
    public partial class ModelBase : IModelBase
    {

        private Dictionary<string, object> _cacheBackup;

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

        protected void SetValue<T>(ref T value, ref T newValue, Action onChanged = null, [CallerMemberName] string name = null)
        {
            if (newValue != null)
            {
                if (!newValue.Equals(value))
                {
                    RaisePropertyChanging(name);
                    value = newValue;
                    RaisePropertyChanged(name);
                }
            }
            else
            {
                value = default(T);
            }
        }       
    }

    public class ModelBase<TKey> : ModelBase
    {

        private TKey _id;
        [Key]
        [Column("Id")]
        public TKey Id { get { return _id; } set { SetValue<TKey>(ref _id, ref _id); } }

        [Column("CreatedDate")]
        public DateTime DateCreated { get; set; }
        [Column("LastModifiedDate")]
        public DateTime DateLastModified { get; set; }

    }
}
