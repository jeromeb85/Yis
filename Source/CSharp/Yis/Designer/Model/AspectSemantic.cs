using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Designer.Model
{
    public class AspectSemantic
    {
        public AspectSemantic()
        {
            this.DomainSemantic = new HashSet<DomainSemantic>();
        }

        [Key]
        [Column("AspectSemancticId")]
        [ForeignKey("WorkSpace")]
        public Guid Id { get; set; }


        public virtual WorkSpace WorkSpace { get; set; }

        public virtual ICollection<DomainSemantic> DomainSemantic { get; set; }
    }
}
