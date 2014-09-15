using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Extension;
using Yis.Framework.Data.Contract;

namespace Yis.Framework.Data.Memory
{
    public abstract class RepositoryBase<TModel> : IRepository<TModel>
           where TModel : class,new()
    {
        #region Fields

        private readonly DataContextBase Context;

        #endregion Fields

        #region Constructors

        public RepositoryBase(IDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("pas de context donné");
            }

            Context = (DataContextBase)dataContext;
        }

        #endregion Constructors

        #region Methods

        public TModel Create()
        {
            return new TModel();
        }

        public IEnumerable<TModel> GetAll()
        {
            return Context.Get<TModel>();
        }

        public IQueryable<TModel> GetQuery()
        {
            return Context.Get<TModel>().AsQueryable<TModel>();
        }

        #endregion Methods
    }
}