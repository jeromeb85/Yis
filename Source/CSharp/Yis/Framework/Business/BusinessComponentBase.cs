using System.Collections.Generic;
using Yis.Framework.Business.Contract;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Logging.Contract;
using Yis.Framework.Core.Rule.Contract;
using Yis.Framework.Data;
using Yis.Framework.Data.Contract;
using Yis.Framework.Model;

namespace Yis.Framework.Business
{
    public abstract class BusinessComponentBase<TModel, TProvider> : IBusinessComponentBase<TModel>
        where TProvider : IRepository<TModel>
        where TModel : IModel
    {
        #region Fields

        protected IDataContext DataContext { get; set; }

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

        #endregion Fields

        #region Constructors

        public BusinessComponentBase(IDataContext dataContext)
        {
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<TModel> GetAll()
        {
            return Provider.GetAll();
        }

        #endregion Methods
    }

    public abstract class BusinessComponentBase<TModel, TProvider, TDataContext> : BusinessComponentBase<TModel, TProvider>
        where TProvider : IRepository<TModel>
        where TModel : IModel
        where TDataContext : IDataContext
    {
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
    }
}