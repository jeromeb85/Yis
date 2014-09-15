using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Extension;

namespace Yis.Framework.Data.Memory
{
    internal class DataStore : IEnumerable<KeyValuePair<Type, IList<object>>>
    {
        #region Fields

        private IDictionary<Type, IList<object>> _data;

        #endregion Fields

        #region Properties

        private IDictionary<Type, IList<Object>> Data
        {
            get
            {
                if (_data.IsNull())
                {
                    _data = new Dictionary<Type, IList<object>>();
                }
                return _data;
            }
        }

        #endregion Properties

        #region Methods

        public void Clear()
        {
            this.Data.Clear();
        }

        public bool Contains<T>(T entity)
        {
            return this.Contains(entity, EqualityComparer<T>.Default);
        }

        public bool Contains<T>(T entity, IEqualityComparer<T> comparer)
        {
            var type = entity.GetType();

            if (this.Data.ContainsKey(type))
            {
                var list = this.Data[type];
                return list.Cast<T>().Contains(entity, comparer);
            }

            return false;
        }

        public IEnumerator<KeyValuePair<Type, IList<object>>> GetEnumerator()
        {
            return this.Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerable<T> List<T>()
        {
            if (this.Data.ContainsKeyImplementing<T>())
            {
                return this.Data.GetInstancesImplementing<T>();
            }
            else
            {
                return Enumerable.Empty<T>();
            }
        }

        public void Remove(object entity)
        {
            var type = entity.GetType();

            if (this.Data.ContainsKey(type))
            {
                var list = this.Data[type];
                list.Remove(entity);
            }
        }

        public void Save(object entity)
        {
            this.SaveToInternalStore(entity);
        }

        internal void Merge(DataStore dataStore)
        {
            foreach (var store in dataStore.Data)
            {
                foreach (var d in store.Value)
                {
                    this.SaveToInternalStore(d);
                }
            }
        }

        private void SaveToInternalStore(object entity)
        {
            var type = entity.GetType();

            if (!this.Data.ContainsKey(type))
            {
                this.Data.Add(type, new List<object>());
            }

            var list = this.Data[type];
            if (!list.Contains(entity))
            {
                list.Add(entity);
            }
            else
            {
                var index = list.IndexOf(entity);
                list[index] = entity;
            }
        }

        #endregion Methods
    }
}