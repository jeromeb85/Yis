using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Extension;
using Yis.Framework.Data.Contract;

namespace Yis.Framework.Data.Cache
{
    public abstract class RepositoryBase<TModel> : IRepository<TModel>
           where TModel : class
    {
        #region Fields

        private static List<TModel> _cache;

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

        #region Properties

        public static List<TModel> Cache
        {
            get
            {
                if (_cache.IsNull())
                    _cache = new List<TModel>();
                return _cache;
            }
        }

        #endregion Properties

        #region Methods

        public IEnumerable<TModel> GetAll()
        {
            throw new ArgumentNullException("pas");
        }

        #endregion Methods
    }
}