using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Designer.Model
{
    [Table("WorkSpace")]
    public partial class WorkSpace
    {
        
        [Key]
        [Column("WorkSpaceId")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength=5)]
        [Index("WorkSpace_Idx_001", 1, IsUnique = true)]        
        public string Name { get; set; }

        //public virtual AspectSemantic AspectSemantic { get; set; }
    }
}
