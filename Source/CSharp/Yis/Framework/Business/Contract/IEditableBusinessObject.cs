using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Yis.Framework.Business.Contract
{
    public interface IEditableBusinessObject : IBusinessObject, IEditableObject, IValidatableObject, IRevertibleChangeTracking
    {
    }
}