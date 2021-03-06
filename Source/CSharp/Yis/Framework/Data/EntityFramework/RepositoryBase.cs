﻿using System;
using System.Collections.Generic;
using System.Data.Entity;

//using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Yis.Framework.Data.Contract;

namespace Yis.Framework.Data.EntityFramework
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
           where TEntity : class
    {
        #region Fields

        private readonly DbContext _dbContext;

        private readonly IDbSet<TEntity> _dbSet;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepositoryBase{TModel, TKey}" /> class.
        /// </summary>
        /// <param name="dbContext">The db context. The caller is responsible for correctly disposing the <see cref="DbContext"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="dbContext" /> is <c>null</c>.</exception>
        public RepositoryBase(IDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("pas de context donné");
            }

            _dbContext = (DbContext)dataContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds the specified entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entity"/> is <c>null</c>.</exception>
        public virtual void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Counts entities with the specified criteria.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The number of entities that match the criteria.</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            predicate = EnsureValidatePredicate(predicate);

            return GetQuery().Count(predicate);
        }

        /// <summary>
        /// Gets an new entity instance, which may be a proxy if the entity meets the proxy requirements and the underlying context is configured to create proxies.
        /// <para />
        /// Note that the returned proxy entity is NOT added or attached to the set.
        /// </summary>
        /// <returns>The proxy entity</returns>
        public virtual TEntity Create()
        {
            return _dbContext.Set<TEntity>().Create();
        }

        /// <summary>
        /// Deletes the specified entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entity" /> is <c>null</c>.</exception>
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Deletes all entities that match the predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="predicate" /> is <c>null</c>.</exception>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
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

        // private readonly string _entitySetName;
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Enumerable of all matching entities.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="predicate" /> is <c>null</c>.</exception>
        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            predicate = EnsureValidatePredicate(predicate);

            return GetQuery(predicate);
        }

        /// <summary>
        /// Gets the first entity based on the matching criteria.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entity or <c>null</c> if no entity matches the criteria.</returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate = null)
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
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = GetQuery();
            predicate = EnsureValidatePredicate(predicate);

            return query.FirstOrDefault(predicate);
        }

        ///// <summary>
        ///// Gets a specific entity by it's primary key value.
        ///// </summary>
        ///// <param name="keyValue">The key value.</param>
        ///// <returns>The entity or <c>null</c> if the entity could not be found.</returns>
        //public virtual TEntity GetByKey(TKey keyValue)
        //{
        //    return _dbSet.Find(keyValue);
        //}

        /// <summary>
        /// Gets the default query for this repository.
        /// </summary>
        /// <returns>The default queryable for this repository.</returns>
        public virtual IQueryable<TEntity> GetQuery()
        {
            //_dbContext.cre

            //var objectContext = ((IObjectContextAdapter) _dbContext).ObjectContext;
            //var objectContext = _dbContext.GetObjectContext();
            //return objectContext.CreateQuery<TModel>(_entitySetName);
            return _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets a customized query for this repository.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The customized queryable for this repository.</returns>
        public virtual IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetQuery();
            return query.Where(predicate);
        }

        IEnumerable<TEntity> IRepository<TEntity>.GetAll()
        {
            return GetQuery().ToList<TEntity>();
        }

        /// <summary>
        /// Gets a single entity based on the matching criteria.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entity or <c>null</c> if no entity matches the criteria.</returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate = null)
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
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = GetQuery();
            predicate = EnsureValidatePredicate(predicate);

            return query.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Updates changes of the existing entity.
        /// <para />
        /// Note that this method does not actually call <c>SaveChanges</c>, but only updates the entity in the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entity" /> is <c>null</c>.</exception>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Set<TEntity>().Attach(entity);

            //var objectContext = ((IObjectContextAdapter)_dbContext).ObjectContext;

            //object originalItem;
            //var key = objectContext.CreateEntityKey(_entitySetName, entity);
            //if (objectContext.TryGetObjectByKey(key, out originalItem))
            //{
            //    objectContext.ApplyCurrentValues(key.EntitySetName, entity);
            //}
        }

        /// <summary>
        /// Ensures a validate predicate.
        /// <para />
        /// If the <paramref name="predicate"/> is <c>null</c>, this method will create a default predicate which
        /// is always true.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The ensured valid predicate.</returns>
        private Expression<Func<TEntity, bool>> EnsureValidatePredicate(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }

            return predicate;
        }

        /// <summary>
        /// Gets all entities available in the repository.
        /// <para />
        /// Not that this method executes the default query returned by <see cref="GetQuery()" />/.
        /// </summary>
        /// <returns>Enumerable of all entities.</returns>
        private IQueryable<TEntity> GetAll()
        {
            return GetQuery();
        }

        #endregion Methods
    }
}