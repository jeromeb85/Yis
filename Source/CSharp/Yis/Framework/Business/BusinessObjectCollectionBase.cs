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

namespace Yis.Framework.Business
{
    public abstract class BusinessObjectCollectionBase
    {
        private static IServiceLocator _locator;

        private static ILog _log;

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
    }

    public abstract class BusinessObjectCollectionBase<TMe, TBusinessObject> : BusinessObjectCollectionBase
    where TMe : BusinessObjectCollectionBase<TMe, TBusinessObject>, new()
    {
        private IList<TBusinessObject> _list;

        public IList<TBusinessObject> List
        {
            get
            {
                if (_list.IsNull())
                {
                    _list = new List<TBusinessObject>();
                }

                return _list;
            }
        }

        #region Methods

        public static TMe New()
        {
            return new TMe();
        }

        #endregion Methods
    }
}