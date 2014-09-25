using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Yis.Framework.Model.Contract;

namespace Yis.Framework.Model
{
    public abstract partial class ModelBase : IModel
    {
    }

    public class ModelBase<TKey> : ModelBase, IModel<TKey>
    {
        #region Properties

        [Key]
        [Column("Id")]
        public TKey Id { get; set; }

        #endregion Properties

        //[Column("CreatedDate")]
        //public DateTime DateCreated { get; set; }
        //[Column("LastModifiedDate")]
        //public DateTime DateLastModified { get; set; }
    }
}