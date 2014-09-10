using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

namespace Yis.Designer.Software.Model
{
    public class Class : ModelBase<Guid>
    {
        public Guid NameSpaceId { get; set; }

        public string Name { get; set; }
    }
}