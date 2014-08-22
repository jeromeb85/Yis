using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

namespace Yis.Framework.Data.EntityFramework
{
    public class EntityRepositoryBase<TModel, TKey> : IRepository<TModel, TKey>
           where TModel : class
    {
        #region Fields
        private readonly DbContext _dbContext;

        private readonly string _entitySetName;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepositoryBase{TModel, TKey}" /> class.
        /// </summary>
        /// <param name="dbContext">The db context. The caller is responsible for correctly disposing the <see cref="DbContext"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="dbContext" /> is <c>null</c>.</exception>
        public EntityRepositoryBase(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("pas de context donné");
            }

            _dbContext = dbContext;

            _entitySetName = dbContext.GetEntitySetName<TModel>();
        }
        #endregion

        #region IEntityRepository<TEntity,TPrimaryKey> Members
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Gets a specific entity by it's primary key value.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        /// <returns>The entity or <c>null</c> if the entity could not be found.</returns>
        public virtual TModel GetByKey(TKey keyValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the default query for this repository.
        /// </summary>
        /// <returns>The default queryable for this repository.</returns>
        public virtual IQueryable<TModel> GetQuery()
        {
            //_dbContext.cre
    
            var objectContext = ((IObjectContextAdapter) _dbContext).ObjectContext; 
            //var objectContext = _dbContext.GetObjectContext();
            return objectContext.CreateQuery<TModel>(_entitySetName);
        }

        /// <summary>
        /// Gets a customized query for this repository.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The customized queryable for this repository.</returns>
        public virtual IQueryable<TModel> GetQuery(Expression<Func<TModel, bool>> predicate)
        {
            var query = GetQuery();
            return query.Where(predicate);
        }

        /// <summary>
        /// Gets a single entity based on the matching criteria.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entity or <c>null</c> if no entity matches the criteria.</returns>
        public virtual TModel Single(Expression<Func<TModel, bool>> predicate = null)
        {
            var query = GetQuery();
            predicate = EnsureValidatePredicate(predicate);

            return query.Single(predicate);
        }

        /// <summary>
        /// Gets a single entity based on the matching criteria.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entity or <c>null</c> if no entity matches the criteria.</returns>
        public virtual TModel SingleOrDefault(Expression<Func<TModel, bool>> predicate = null)
        {
            var query = GetQuery();
            predicate = EnsureValidatePredicate(predicate);

            return query.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Gets the first entity based on the matching criteria.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entity or <c>null</c> if no entity matches the criteria.</returns>
        public virtual TModel First(Expression<Func<TModel, bool>> predicate = null)
        {
            var query = GetQuery();
            predicate = EnsureValidatePredicate(predicate);

            return query.First(predicate);
        }

        /// <summary>
        /// Gets the first entity based on the matching criteria.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entity or <c>null</c> if no entity matches the criteria.</returns>
        public virtual TModel FirstOrDefault(Expression<Func<TModel, bool>> predicate = null)
        {
            var query = GetQuery();
            predicate = EnsureValidatePredicate(predicate);

            return query.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Gets an new entity instance, which may be a proxy if the entity meets the proxy requirements and the underlying context is configured to create proxies.
        /// <para />
        /// Note that the returned proxy entity is NOT added or attached to the set.
        /// </summary>
        /// <returns>The proxy entity</returns>
        public virtual TModel Create()
        {
            return _dbContext.Set<TModel>().Create();
        }

        /// <summary>
        /// Adds the specified entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entity"/> is <c>null</c>.</exception>
        public virtual void Add(TModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Set<TModel>().Add(entity);
        }

        /// <summary>
        /// Attaches the specified entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to attach.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entity" /> is <c>null</c>.</exception>
        public virtual void Attach(TModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Set<TModel>().Attach(entity);
        }

        /// <summary>
        /// Deletes the specified entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entity" /> is <c>null</c>.</exception>
        public virtual void Delete(TModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Set<TModel>().Remove(entity);
        }

        /// <summary>
        /// Deletes all entities that match the predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="predicate" /> is <c>null</c>.</exception>
        public virtual void Delete(Expression<Func<TModel, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException();
            }

            var entities = Find(predicate);

            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// Updates changes of the existing entity.
        /// <para />
        /// Note that this method does not actually call <c>SaveChanges</c>, but only updates the entity in the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entity" /> is <c>null</c>.</exception>
        public virtual void Update(TModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            var objectContext = ((IObjectContextAdapter)_dbContext).ObjectContext; 

            object originalItem;
            var key = objectContext.CreateEntityKey(_entitySetName, entity);
            if (objectContext.TryGetObjectByKey(key, out originalItem))
            {
                objectContext.ApplyCurrentValues(key.EntitySetName, entity);
            }
        }

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Enumerable of all matching entities.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="predicate" /> is <c>null</c>.</exception>
        public virtual IQueryable<TModel> Find(Expression<Func<TModel, bool>> predicate)
        {
            predicate = EnsureValidatePredicate(predicate);

            return GetQuery(predicate);
        }

        /// <summary>
        /// Gets all entities available in the repository.
        /// <para />
        /// Not that this method executes the default query returned by <see cref="GetQuery()" />/.
        /// </summary>
        /// <returns>Enumerable of all entities.</returns>
        public virtual IQueryable<TModel> GetAll()
        {
            return GetQuery();
        }

        /// <summary>
        /// Counts entities with the specified criteria.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The number of entities that match the criteria.</returns>
        public virtual int Count(Expression<Func<TModel, bool>> predicate = null)
        {
            predicate = EnsureValidatePredicate(predicate);

            return GetQuery().Count(predicate);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Ensures a validate predicate.
        /// <para />
        /// If the <paramref name="predicate"/> is <c>null</c>, this method will create a default predicate which
        /// is always true.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The ensured valid predicate.</returns>
        private Expression<Func<TModel, bool>> EnsureValidatePredicate(Expression<Func<TModel, bool>> predicate)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }

            return predicate;
        }
        #endregion
    }
}
