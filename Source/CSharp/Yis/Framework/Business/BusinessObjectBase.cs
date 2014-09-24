using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Data.Contract;
using Yis.Framework.Model.Contract;

namespace Yis.Framework.Business
{
    public abstract class BusinessObjectBase<TModel, TProvider, TDataContext> : BusinessComponentBase<TModel, TProvider, TDataContext>
        where TProvider : IRepository<TModel>
        where TModel : class,IModel
        where TDataContext : IDataContext
    {
        #region Constructors

        public BusinessObjectBase()
            : base()
        {
        }

        #endregion Constructors
    }
}