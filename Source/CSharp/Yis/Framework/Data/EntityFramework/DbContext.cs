using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Caching;
using Yis.Framework.Core.Fluent;

namespace Yis.Framework.Data.EntityFramework
{
    public class DbContext : System.Data.Entity.DbContext
    {

        private static readonly ICacheStorage<Tuple<Type, Type>, string> _entitySetNameCache = new CacheStorage<Tuple<Type, Type>, string>();
        private static readonly ICacheStorage<Tuple<Type, Type>, string> _entityKeyPropertyNameCache = new CacheStorage<Tuple<Type, Type>, string>();

        #region Constructor

        public DbContext() : base() { }
        public DbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        #endregion

        public ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }

        public string GetEntitySetName<TEntity>()
        {
            return GetEntitySetName(typeof(TEntity));
        }

        public string GetEntitySetName(Type entityType)
        {
            var entitySetName = _entitySetNameCache.GetFromCacheOrFetch(new Tuple<Type, Type>(this.GetType(), entityType), () =>
            {
                return ObjectContext.MetadataWorkspace.GetEntityContainer(ObjectContext.DefaultContainerName, DataSpace.CSpace).BaseEntitySets.First(bes => bes.ElementType.Name == entityType.Name).Name;
            });

            return entitySetName;
        }

    }
}
