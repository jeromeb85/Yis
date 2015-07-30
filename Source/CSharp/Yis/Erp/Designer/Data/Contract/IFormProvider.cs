using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Data.Contract;

namespace Yis.Erp.Designer.Data.Contract
{
    public interface IFormProvider : IRepository<Model.Form>
    {
        Model.Form GetById(Guid id);
    }
}
