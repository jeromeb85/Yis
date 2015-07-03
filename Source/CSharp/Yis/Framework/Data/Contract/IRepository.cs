using System.Collections.Generic;

namespace Yis.Framework.Data.Contract
{
    public interface IRepository
    {
    }

    public interface IRepository<TModel> : IRepository
   where TModel : class
    {
        #region Methods

        // TModel FirstOrDefault(Expression<Func<TModel, bool>> predicate = null);
        void Add(TModel entity);

        TModel Create();

        IEnumerable<TModel> GetAll();

        #endregion Methods

        // IQueryable<TModel> GetQuery(Expression<Func<TModel, bool>> predicate);

        // TModel Single(Expression<Func<TModel, bool>> predicate = null);

        // TModel SingleOrDefault(Expression<Func<TModel, bool>> predicate = null);

        //    TModel First(Expression<Func<TModel, bool>> predicate = null);
        //void Delete(TModel entity);

        //void Delete(Expression<Func<TModel, bool>> predicate);

        // void Update(TModel entity);

        // IQueryable<TModel> Find(Expression<Func<TModel, bool>> predicate); int
        // Count(Expression<Func<TModel, bool>> predicate = null);
    }
}