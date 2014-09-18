using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.IoC.Contract;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Logging.Contract;
using Yis.Framework.Data.Contract;

namespace Yis.Framework.Data.Memory
{
    public abstract class RepositoryBase<TModel> : IRepository<TModel>
           where TModel : class,new()
    {
        #region Fields

        private static IServiceLocator _locator;

        private static ILog _log;

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

        protected static IServiceLocator Locator
        {
            get
            {
                if (_locator.IsNull()) _locator = Resolver.Resolve<IServiceLocator>();
                return _locator;
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

        #endregion Properties

        #region Methods

        public void Add(TModel entity)
        {
            Context.Add<TModel>(entity);
        }

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