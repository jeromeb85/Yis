using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Model
{
    public class ModelBase : IModelBase
    {
    }

    public class ModelBase<TKey>  : ModelBase
    {
        [Key]
        [Column("Id")]
        public TKey Id { get; set; }
    }
}
