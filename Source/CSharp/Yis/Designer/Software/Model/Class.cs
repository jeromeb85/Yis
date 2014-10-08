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

        public string BaseType { get; set; }

        public Guid Id { get; set; }

        public IList<string> Implement { get; set; }

        public IList<string> Import { get; set; }

        public string Name { get; set; }

        public Guid NameSpaceId { get; set; }

        public Scope Scope { get; set; }

        #endregion Properties
    }
}