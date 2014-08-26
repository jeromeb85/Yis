using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Model
{
    public partial class ModelBase : IDataErrorInfo
    {
        #region IDataErrorInfo
        [IgnoreDataMember]
        public string Error
        {
            get
            {
                ValidationContext context = new ValidationContext(this, null, null);
                ICollection<ValidationResult> errors = new List<ValidationResult>();
                if (Validator.TryValidateObject(this, context, errors, true))
                    return string.Empty;
                else
                    return string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage));
            }
        }

        public string this[string columnName]
        {
            get
            {
                object value = TypeDescriptor.GetProperties(this)[columnName].GetValue(this);

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = columnName };
                ICollection<ValidationResult> errors = new List<ValidationResult>();
                if (Validator.TryValidateProperty(value, context, errors))
                    return string.Empty;
                else
                    return string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage));
            }
        }
        #endregion


    }

}
