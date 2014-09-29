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
using Yis.Framework.Data;
using Yis.Framework.Data.Contract;
using Yis.Framework.Model.Contract;

namespace Yis.Framework.Business
{
    public abstract class BusinessObjectCollectionBase
    {
        #region Fields

        private static IServiceLocator _locator;

        private static ILog _log;

        #endregion Fields

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
    }

    public abstract class BusinessObjectCollectionBase<TMe, TBusinessObject> : BusinessObjectCollectionBase, IEnumerable<TBusinessObject>
        where TMe : BusinessObjectCollectionBase<TMe, TBusinessObject>
        where TBusinessObject : BusinessObjectBase
    {
        #region Fields

        private IEnumerable<TBusinessObject> _list;

        #endregion Fields

        #region Constructors

        public BusinessObjectCollectionBase(IEnumerable<TBusinessObject> businessObject)
            : base()
        {
            _list = businessObject;
        }

        #endregion Constructors

        #region Properties

        public IEnumerable<TBusinessObject> List
        {
            get
            {
                if (_list.IsNull())
                {
                    _list = new List<TBusinessObject>();
                }

                return _list;
            }

            private set
            {
                _list = value;
            }
        }

        #endregion Properties

        #region Methods

        public IEnumerator<TBusinessObject> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Methods
    }

    public abstract class BusinessObjectCollectionBase<TMe, TBusinessObject, TModel, TProvider, TDataContext> : BusinessObjectCollectionBase<TMe, TBusinessObject>
        where TMe : BusinessObjectCollectionBase<TMe, TBusinessObject, TModel, TProvider, TDataContext>
        where TBusinessObject : BusinessObjectBase<TBusinessObject, TModel, TProvider, TDataContext>
        where TModel : class,IModel
        where TProvider : IRepository<TModel>
        where TDataContext : IDataContext
    {
        #region Fields

        private static IDataContext _dataContext;

        private static TProvider _provider;

        #endregion Fields

        #region Constructors

        public BusinessObjectCollectionBase(IEnumerable<TBusinessObject> businessObject)
            : base(businessObject)
        {
        }

        #endregion Constructors

        #region Properties

        public static IDataContext DataContext
        {
            get
            {
                if (_dataContext.IsNull())
                {
                    _dataContext = !Resolver.IsRegistered<TDataContext>() ? Locator.ResolveAndCreateType<TDataContext>() : Resolver.Resolve<TDataContext>();
                    if (!Resolver.IsRegistered<TDataContext>())
                    {
                        Resolver.Register<TDataContext>((TDataContext)_dataContext);
                    }
                }
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

        protected static TProvider Provider
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

        #endregion Properties

        #region Methods

        public static TMe GetAll()
        {
            List<TBusinessObject> list = new List<TBusinessObject>();

            foreach (var item in Provider.GetAll())
            {
                TBusinessObject bo = Activator.CreateInstance<TBusinessObject>();
                bo.Load(item);
                list.Add(bo);
            }

            return (TMe)Activator.CreateInstance(typeof(TMe), new object[] { list });
        }

        #endregion Methods
    }
}