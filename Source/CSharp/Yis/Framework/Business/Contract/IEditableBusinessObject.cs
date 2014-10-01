using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Business.Contract
{
    public interface IEditableBusinessObject : IBusinessObject, IEditableObject, IValidatableObject, IRevertibleChangeTracking
    {
    }

    public interface IEditableBusinessObject<TProvider> : IEditableBusinessObject
    {
        #region Methods

        void save();

        #endregion Methods
    }
}