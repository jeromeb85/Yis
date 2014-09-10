using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

namespace Yis.Designer.Software.Model
{
    public class NameSpace : ModelBase<Guid>
    {
        public string Name { get; set; }

        public Guid ParentNameSpaceId { get; set; }

        public NameSpace ParentNameSpace { get; set; }

        public virtual ICollection<NameSpace> ChrildrenNameSpace { get; set; }

        public virtual ICollection<Class> Class { get; set; }
    }
}