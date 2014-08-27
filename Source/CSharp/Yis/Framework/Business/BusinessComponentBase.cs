using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.IoC;
using Yis.Framework.Data;
using Yis.Framework.Model;
using Yis.Framework.Rule;

namespace Yis.Framework.Business
{
    public abstract class BusinessComponentBase<TModel, TProvider> : IBusinessComponentBase<TModel>
        where TProvider : IRepository<TModel>
        where TModel : ModelBase
    {
        private IDataContext DataContext { get; set; }


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


        //private RuleValidator _validator;
        //protected RuleValidator Validator
        //{
        //    get
        //    {
        //        if (_validator == null)
        //            _validator = new RuleValidator();
        //        return _validator;
        //    }
        //}
      
        #region Constructeurs

        public BusinessComponentBase(string nameDataContext) : this()
        {
            DataContext = DependencyResolver.Resolve<IDataContext>(nameDataContext);            
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
