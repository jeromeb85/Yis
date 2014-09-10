using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yis.Framework.Model;

namespace Yis.Designer.Model
{
    [Table("WorkSpace")]
    [Serializable]
    public partial class WorkSpace : ModelBase<Guid>
    {
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "La longueur doit être comprise entre 5 et 50")]
        [Display(Name = "Name")]
        [Index("WorkSpace_Idx_001", 1, IsUnique = true)]
        public string Name { get; set; }

        //public virtual AspectSemantic AspectSemantic { get; set; }
    }
}