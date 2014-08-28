using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Presentation.ViewModel
{
    public abstract partial class ViewModelBase : IDataErrorInfo
    {
        public string Error
        {
            get {
                if (!HasErrors)
                    return string.Empty;
                else
                    return string.Join(Environment.NewLine, _errors.Select(e => e.Value.Select(d => d.ErrorMessage)));
            }
        }

        public string this[string columnName]
        {
            get { return string.Join(Environment.NewLine, GetErrors(columnName).Select(e => e.ErrorMessage)); }
        }
    }
}
