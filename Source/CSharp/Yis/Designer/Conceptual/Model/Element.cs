using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

namespace Yis.Designer.Conceptual.Model
{
    public abstract class Element : ModelBase
    {
        #region Properties

        public Guid Id { get; set; }



        #endregion Properties
    }
}