using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

namespace Yis.Designer.Software.Model
{
    public class Class : ModelBase
    {
        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid NameSpaceId { get; set; }

        #endregion Properties
    }
}