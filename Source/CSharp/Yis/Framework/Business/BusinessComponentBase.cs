using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.IoC;
using Yis.Framework.Data;
using Yis.Framework.Model;
using Yis.Framework.Core.Rule;
using Yis.Framework.Business.Contract;
using Yis.Framework.Data.Contract;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Logging.Contract;
using Yis.Framework.Core.Rule.Contract;

namespace Yis.Framework.Business
{
    public abstract class BusinessComponentBase<TModel, TProvider> : IBusinessComponentBase<TModel>
        where TProvider : IRepository<TModel>
        where TModel : ModelBase
    {
        #region Fields

        private IDataContext DataContext { get; set; }

        private static IServiceLocator _locator;
        protected static IServiceLocator Locator
        {
            get
            {
                if (_locator.IsNull()) _locator = Resolver.Resolve<IServiceLocator>();
                return _locator;
            }
        }

        private static ILog _log;
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

        private TProvider _provider;
        protected TProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = UnitOfWork.GetRepository<TProvider>(DataContext);
                }
                return _provider;
            }
            private set { _provider = value; }
        }


        private IRuleValidator _validator;
        protected IRuleValidator Validator
        {
            get
            {
                if (_validator == null)
                    _validator = Locator.ResolveAndCreateType<IRuleValidator>();
                return _validator;
            }
        }
        #endregion

        #region Constructors

        public BusinessComponentBase(string nameDataContext)
            : this()
        {
            DataContext = Resolver.Resolve<IDataContext>(nameDataContext);
        }

        private BusinessComponentBase()
        {

        }

        #endregion

        public IEnumerable<TModel> GetAll()
        {
            return Provider.GetAll();
        }
    }
}
