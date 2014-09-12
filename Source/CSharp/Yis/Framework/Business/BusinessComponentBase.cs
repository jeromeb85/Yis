using System.Collections.Generic;
using Yis.Framework.Business.Contract;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Logging.Contract;
using Yis.Framework.Core.Validation.Contract;
using Yis.Framework.Data;
using Yis.Framework.Data.Contract;
using Yis.Framework.Model.Contract;

namespace Yis.Framework.Business
{
    public abstract class BusinessComponentBase<TModel, TProvider> : IBusinessComponentBase<TModel>
        where TProvider : IRepository<TModel>
        where TModel : class,IModel
    {
        #region Fields

        private static IServiceLocator _locator;

        private static ILog _log;

        private IDataContext _dataContext;
        private TProvider _provider;

        private IRuleValidator _validator;

        #endregion Fields

        #region Constructors

        public BusinessComponentBase(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        #endregion Constructors

        #region Properties

        public IDataContext DataContext
        {
            get
            {
                return _dataContext;
            }
            set
            {
                if (value != _dataContext)
                {
                    _dataContext = value;
                    _provider = default(TProvider);
                }
            }
        }

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

        protected TProvider Provider
        {
            get
            {
                if (_provider.IsNull())
                {
                    _provider = UnitOfWork.GetRepository<TProvider>(DataContext);
                }
                return _provider;
            }
            private set { _provider = value; }
        }

        protected IRuleValidator Validator
        {
            get
            {
                if (_validator == null)
                    _validator = Locator.ResolveAndCreateType<IRuleValidator>();
                return _validator;
            }
        }

        #endregion Properties

        #region Methods

        public IEnumerable<TModel> GetAll()
        {
            return Provider.GetAll();
        }

        #endregion Methods
    }

    public abstract class BusinessComponentBase<TModel, TProvider, TDataContext> : BusinessComponentBase<TModel, TProvider>
        where TProvider : IRepository<TModel>
        where TModel : class,IModel
        where TDataContext : IDataContext
    {
        #region Constructors

        public BusinessComponentBase()
            : base(Resolver.IsRegistered<TDataContext>() ? Locator.ResolveAndCreateType<TDataContext>() : Resolver.Resolve<TDataContext>())
        {
            if (!Resolver.IsRegistered<TDataContext>())
            {
                Resolver.Register<TDataContext>((TDataContext)DataContext);
            }
        }

        public BusinessComponentBase(string nameOrConnectionString)
            : base(Resolver.IsRegistered<TDataContext>() ? Locator.ResolveAndCreateType<TDataContext>(new object[] { nameOrConnectionString }) : Resolver.Resolve<TDataContext>())
        {
            if (!Resolver.IsRegistered<TDataContext>())
            {
                Resolver.Register<TDataContext>((TDataContext)DataContext);
            }
        }

        #endregion Constructors
    }
}