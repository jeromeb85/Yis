using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

namespace Yis.Framework.Data
{
    public interface IRepository
    {
    }

    public interface IRepository<TModel> : IRepository
   where TModel : class
    {

        #region Methods
        IQueryable<TModel> GetQuery();
        IQueryable<TModel> GetQuery(Expression<Func<TModel, bool>> predicate);
        TModel Single(Expression<Func<TModel, bool>> predicate = null);
        TModel SingleOrDefault(Expression<Func<TModel, bool>> predicate = null);
        TModel First(Expression<Func<TModel, bool>> predicate = null);
        TModel FirstOrDefault(Expression<Func<TModel, bool>> predicate = null);
        TModel Create();
        void Add(TModel entity);
        void Attach(TModel entity);
        void Delete(TModel entity);
        void Delete(Expression<Func<TModel, bool>> predicate);
        void Update(TModel entity);
        IQueryable<TModel> Find(Expression<Func<TModel, bool>> predicate);
        IQueryable<TModel> GetAll();
        int Count(Expression<Func<TModel, bool>> predicate = null);
        #endregion
    }


    public interface IRepository<TModel, TKey> : IRepository<TModel>
        where TModel : class
    {
        #region Methods
        TModel GetByKey(TKey keyValue);
        #endregion
    }
}
