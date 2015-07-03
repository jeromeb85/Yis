using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.IoC.Contract;
using Yis.Framework.Core.Logging.Contract;
using Yis.Framework.Core.Serialization.Contract;

namespace Yis.Framework.Data.Memory
{
    /// <summary>
    /// Gestion du stockage des données pour le repository memory Cette classe traite l'information
    /// en mémoire et peut persister les informations dans un fichier
    /// </summary>
    internal class DataStore
    {
        #region Constructors + Destructors

        /// <summary>
        /// Constructeur de <see cref="DataStore"/>
        /// </summary>
        /// <param name="path">Chemin pour persister les informations</param>
        public DataStore(string path = null)
        {
            Path = path;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        #endregion Constructors + Destructors

        #region Fields

        private static ILog _log;
        private static ISerializer _serializer;
        private readonly string Path;
        private IDictionary<Type, IList<object>> _data;
        private IList<Type> _typeLoaded;

        #endregion Fields

        #region Properties

        public bool IsPersistent
        {
            get
            {
                return !String.IsNullOrEmpty(Path);
            }
        }

        protected static ILog Log
        {
            get
            {
                if (_log.IsNull()) _log = Resolver.Resolve<ILog>();
                return _log;
            }
        }

        protected static IDependencyResolver Resolver
        {
            get { return DependencyResolverManager.Default; }
        }

        protected static ISerializer Serializer
        {
            get
            {
                if (_serializer.IsNull()) _serializer = Resolver.Resolve<ISerializer>();
                return _serializer;
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

        private IList<Type> TypeLoaded
        {
            get
            {
                if (_typeLoaded.IsNull())
                {
                    _typeLoaded = new List<Type>();
                }
                return _typeLoaded;
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
            if (!IsLoaded(type)) Load(type);

            if (this.Data.ContainsKey(type))
            {
                var list = this.Data[type];
                return list.Cast<T>().Contains(entity, comparer);
            }

            return false;
        }

        public IEnumerable<T> List<T>()
        {
            if (!IsLoaded(typeof(T))) Load(typeof(T));

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
            if (!IsLoaded(type)) Load(type);

            if (this.Data.ContainsKey(type))
            {
                var list = this.Data[type];
                list.Remove(entity);
            }

            if (IsPersistent)
                Persit(entity);
        }

        public void Save(object entity)
        {
            var type = entity.GetType();
            if (!IsLoaded(type)) Load(type);

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

        private bool IsLoaded(Type type)
        {
            if (!IsPersistent) return true;

            return TypeLoaded.Contains(type);
        }

        private void Load(Type type)
        {
            if (!this.Data.ContainsKey(type))
            {
                this.Data.Add(type, new List<object>());
            }

            if (IsPersistent && File.Exists(Path + @"\" + type.FullName + @".xml"))
            {
                this.Data[type] = Serializer.DeSerialize<IList<Object>>(Path + @"\" + type.FullName + @".xml");
            }
            TypeLoaded.Add(type);
        }

        private void Persit(object entity)
        {
            var type = entity.GetType();
            var list = this.Data[type];

            Serializer.Serialize<IList<Object>>(list, Path + @"\" + type.FullName + @".xml");
        }

        #endregion Methods
    }
}