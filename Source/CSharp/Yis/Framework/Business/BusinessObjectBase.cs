using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Yis.Framework.Business.Contract;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.IoC;
using Yis.Framework.Core.IoC.Contract;
using Yis.Framework.Core.Locator.Contract;
using Yis.Framework.Core.Logging.Contract;
using Yis.Framework.Core.Validation.Contract;
using Yis.Framework.Data;
using Yis.Framework.Data.Contract;
using Yis.Framework.Model.Contract;

namespace Yis.Framework.Business
{
    public abstract class BusinessObjectBase : IEditableBusinessObject, IDataErrorInfo
    {
        #region Fields

        private static IServiceLocator _locator;
        private static ILog _log;
        private Dictionary<string, object> _cacheBackup;
        private Dictionary<string, object> _cacheProperty;
        private List<ISavableBusinessObject> _childSavable;
        private bool _isChanged;
        private bool _isDelete;
        private bool _isEditing;
        private bool _isReadOnly;
        private IRuleValidator _validator;

        #endregion Fields

        #region Properties

        [IgnoreDataMember]
        [XmlIgnore]
        public string Error
        {
            get
            {
                ValidationContext context = new ValidationContext(this, null, null);
                ICollection<ValidationResult> errors = new List<ValidationResult>();
                if (System.ComponentModel.DataAnnotations.Validator.TryValidateObject(this, context, errors, true))
                    return string.Empty;
                else
                    return string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage));
            }
        }

        [XmlIgnore]
        [IgnoreDataMember]
        public bool IsChanged
        {
            get
            {
                return _isChanged;
            }
            protected set
            {
                if (_isChanged != value)
                {
                    _isChanged = value;
                }
            }
        }

        [XmlIgnore]
        [IgnoreDataMember]
        public bool IsDelete
        {
            get
            {
                return _isDelete;
            }

            protected set
            {
                if (_isDelete != value)
                {
                    _isDelete = value;
                }
            }
        }

        [XmlIgnore]
        [IgnoreDataMember]
        public bool IsEditing
        {
            get
            {
                return _isEditing;
            }

            private set
            {
                if (_isEditing != value)
                {
                    _isEditing = value;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }

            protected set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
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

        protected IList<ISavableBusinessObject> ChildSavable
        {
            get
            {
                if (_childSavable.IsNull())
                {
                    _childSavable = new List<ISavableBusinessObject>();
                }
                return _childSavable;
            }
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

        private Dictionary<string, object> CacheBackup
        {
            get
            {
                if (_cacheBackup.IsNull())
                {
                    _cacheBackup = new Dictionary<string, object>();
                }

                return _cacheBackup;
            }
        }

        private Dictionary<string, object> CacheProperty
        {
            get
            {
                if (_cacheProperty.IsNull())
                {
                    _cacheProperty = new Dictionary<string, object>();
                }

                return _cacheProperty;
            }
        }

        #endregion Properties

        #region Indexers

        public string this[string columnName]
        {
            get
            {
                object value = TypeDescriptor.GetProperties(this)[columnName].GetValue(this);

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = columnName };
                ICollection<ValidationResult> errors = new List<ValidationResult>();
                if (System.ComponentModel.DataAnnotations.Validator.TryValidateProperty(value, context, errors))
                    return string.Empty;
                else
                    return string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage));
            }
        }

        #endregion Indexers

        #region Methods

        public void AcceptChanges()
        {
            _cacheBackup = null;
            IsEditing = false;
        }

        public void BeginEdit()
        {
            if (IsDelete)
                throw new Exception("Pas editable car delete");

            Backup();
            IsEditing = true;
        }

        public void CancelEdit()
        {
            RejectChanges();
        }

        public void EndEdit()
        {
            AcceptChanges();
        }

        public void RejectChanges()
        {
            Restore();
            _cacheBackup = null;
            IsChanged = false;
            IsEditing = false;
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }

        protected T GetProperty<T>(T value)
        {
            return value;
        }

        protected T GetProperty<T>(Func<T> load, Action<T> callBack = null, bool IsChildAutoSave = false, bool IsChildAutoDelete = false, [CallerMemberName] string propertyName = null)
        {
            T value = default(T);

            if (CacheProperty.ContainsKey(propertyName))
            {
                value = (T)CacheProperty[propertyName];
            }
            else
            {
                value = load();
                if (IsChildAutoSave && value is ISavableBusinessObject)
                    ChildSavable.Add(value as ISavableBusinessObject);
                CacheProperty.Add(propertyName, value);

                if (!callBack.IsNull())
                    callBack(value);
            }

            return value;
        }

        protected T GetProperty<T>(ref T value, Func<T> load)
        {
            if (value.IsNull())
            {
                value = load();
            }
            return value;
        }

        protected void SetProperty<T>(ref T value, T newValue)
        {
            if (newValue != null)
            {
                if (!newValue.Equals(value))
                {
                    if (!IsEditing)
                        BeginEdit();
                    if (!IsChanged)
                        IsChanged = true;
                    value = newValue;
                }
            }
            else
            {
                value = default(T);
            }
        }

        protected void SetProperty<T>(Action<T> setValue, T actualValue, T newValue)
        {
            if (newValue != null)
            {
                if (!newValue.Equals(actualValue))
                {
                    if (!IsEditing)
                        BeginEdit();
                    if (!IsChanged)
                        IsChanged = true;
                    setValue(newValue);

                }
            }
            else
            {
                setValue(default(T));
            }
        }

        private void Backup()
        {
            //=> Va a l'encontre dans l'état du LazyLoading... tester si la propriété est charger avant backup

            //_cacheBackup = new Dictionary<string, object>();
            //IEnumerable<PropertyInfo> properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite);
            //foreach (PropertyInfo property in properties)
            //    _cacheBackup.Add(property.Name, property.GetValue(this, null));
        }

        private void Restore()
        {
            //if (_cacheBackup != null)
            //{
            //    IEnumerable<PropertyInfo> properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite);
            //    foreach (PropertyInfo property in properties)
            //        property.SetValue(this, _cacheBackup[property.Name], null);
            //}
        }

        #endregion Methods
    }

    public abstract class BusinessObjectBase<TMe> : BusinessObjectBase
        where TMe : BusinessObjectBase<TMe>
    {
        #region Methods

        public static TMe New()
        {
            return Activator.CreateInstance<TMe>();
        }

        #endregion Methods
    }

    public abstract class BusinessObjectBase<TMe, TModel, TProvider, TDataContext> : BusinessObjectBase<TMe>, ISavableBusinessObject
        where TMe : BusinessObjectBase<TMe, TModel, TProvider, TDataContext>
        where TProvider : IRepository<TModel>
        where TModel : class,IModel
        where TDataContext : IDataContext
    {
        #region Fields

        private static IDataContext _dataContext;

        private static TProvider _provider;

        private TModel _model;

        #endregion Fields

        #region Constructors

        public BusinessObjectBase()
            : base()
        {
            _model = Provider.Create();
            IsNew = true;
        }

        public BusinessObjectBase(TModel model)
            : base()
        {
            Model = model;
            IsNew = false;
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

        public bool IsNew { get; protected set; }

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

        protected TModel Model
        {
            get
            {
                return _model;
            }

            private set
            {
                _model = value;
            }
        }

        #endregion Properties

        #region Methods

        public void Delete(bool directSave = false)
        {
            if (IsEditing)
                CancelEdit();

            IsDelete = true;

            if (directSave)
                Save();
        }

        public void Save()
        {
            if (IsEditing)
                EndEdit();

            using (var uow = new UnitOfWork(DataContext))
            {
                ChildSavable.ForEach((i) => i.Save());

                if (IsDelete)
                {
                }
                else if (IsNew)
                {
                    Provider.Add(ToModel());

                    uow.SaveChanges();
                    IsNew = false;
                    IsChanged = false;
                }
                else if (IsChanged)
                {
                    IsChanged = false;
                }
            }
        }

        public TModel ToModel()
        {
            return Model;
        }

        internal void Load(TModel model)
        {
            Model = model;
            IsNew = false;
        }

        #endregion Methods
    }
}