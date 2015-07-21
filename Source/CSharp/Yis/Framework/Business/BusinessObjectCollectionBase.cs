using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Business.Contract;
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
        where TBusinessObject : BusinessObjectBase<TBusinessObject>
    {
        #region Fields

        private EventHandler<AddedNewEventArgs<TBusinessObject>> _addedNewHandlers = null;

        private ICollection<TBusinessObject> _list;

        #endregion Fields

        #region Constructors

        public BusinessObjectCollectionBase()
            : base()
        {
        }

        public BusinessObjectCollectionBase(ICollection<TBusinessObject> businessObject)
            : base()
        {
            _list = businessObject;
        }

        #endregion Constructors

        #region Events

        public event EventHandler<AddedNewEventArgs<TBusinessObject>> AddedNew
        {
            add
            {
                _addedNewHandlers = (EventHandler<AddedNewEventArgs<TBusinessObject>>)
                  System.Delegate.Combine(_addedNewHandlers, value);
            }
            remove
            {
                _addedNewHandlers = (EventHandler<AddedNewEventArgs<TBusinessObject>>)
                  System.Delegate.Remove(_addedNewHandlers, value);
            }
        }

        #endregion Events

        #region Properties

        protected ICollection<TBusinessObject> List
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

        public TBusinessObject AddNew()
        {
            TBusinessObject item = Activator.CreateInstance<TBusinessObject>();
            List.Add(item);

            OnAddedNew(item);

            return item;
        }

        public IEnumerator<TBusinessObject> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public TBusinessObject GetFirstOrAddNew(Predicate<TBusinessObject> predicate)
        {
            TBusinessObject result;

            if (this.Any((i) => predicate(i)))
            {
                result = this.First((i) => predicate(i));
            }
            else
            {
                result = this.AddNew();
            }

            return result;
        }

        public bool IsExists(Predicate<TBusinessObject> predicate)
        {
            return List.Any((i) => predicate(i));
        }

        public virtual void OnAddedNew(TBusinessObject item)
        {
            if (_addedNewHandlers != null)
            {
                var args = new AddedNewEventArgs<TBusinessObject>(item);
                _addedNewHandlers(this, args);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal void Load(ICollection<TBusinessObject> list)
        {
            List = list;
        }

        #endregion Methods
    }

    public abstract class BusinessObjectCollectionBase<TMe, TBusinessObject, TModel, TProvider, TDataContext> : BusinessObjectCollectionBase<TMe, TBusinessObject>, ISavableBusinessObject
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

        public BusinessObjectCollectionBase(IEnumerable<TModel> model)
            : this(ModelToBusinessObject(model))
        {
        }

        public BusinessObjectCollectionBase()
            : base()
        {
        }

        public BusinessObjectCollectionBase(ICollection<TBusinessObject> businessObject)
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

            TMe result = Activator.CreateInstance<TMe>();
            result.Load(list);

            return result;
        }

        public void Save()
        {
            foreach (var item in List)
            {
                item.Save();
            }
        }

        private static ICollection<TBusinessObject> ModelToBusinessObject(IEnumerable<TModel> list)
        {
            ICollection<TBusinessObject> result = new List<TBusinessObject>();

            foreach (var item in list)
            {
                result.Add(ModelToBusinessObject(item));
            }

            return result;
        }

        private static TBusinessObject ModelToBusinessObject(TModel model)
        {
            TBusinessObject bo = Activator.CreateInstance<TBusinessObject>();
            bo.Load(model);
            return bo;
        }

        #endregion Methods
    }
}