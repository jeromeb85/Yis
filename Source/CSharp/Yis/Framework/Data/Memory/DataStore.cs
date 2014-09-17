using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.Serialization;
using Yis.Framework.Model.Contract;

namespace Yis.Framework.Data.Memory
{
    internal class DataStore : IEnumerable<KeyValuePair<Type, IList<object>>>
    {
        #region Fields

        private readonly string Path;

        private IDictionary<Type, IList<object>> _data;

        #endregion Fields

        #region Constructors

        public DataStore(string path = null)
        {
            Path = path;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (IsPersistent)
                Load();
        }

        #endregion Constructors

        #region Properties

        public bool IsPersistent
        {
            get
            {
                return !String.IsNullOrEmpty(Path);
            }
        }

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

        private void Load()
        {
            //var serializer = new XmlSerializer(typeof(List<Todo>));
            //using (var stream = new FileStream(_filename, FileMode.Open)) {
            //    _items = serializer.Deserialize(stream) as List<Todo>;
        }

        private void Persit(object entity)
        {
            var type = entity.GetType();
            var list = this.Data[type];

            YAXSerializer serializer2 = new YAXSerializer(list.GetType());
            serializer2.SerializeToFile(list, Path + @"\" + type.FullName + @".xml");
            //var serializer = new XmlSerializer();
            //using (var stream = new FileStream(Path + @"\" + type.FullName + @".xml", FileMode.Create))
            //{
            //    serializer.Serialize<IList<object>>(list, stream);
            //}
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

            if (IsPersistent)
                Persit(entity);
        }

        #endregion Methods
    }
}