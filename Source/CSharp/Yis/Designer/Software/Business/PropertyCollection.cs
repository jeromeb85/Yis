using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Data.Contract;
using Yis.Framework.Business;

namespace Yis.Designer.Software.Business
{
    public class PropertyCollection : BusinessObjectCollectionBase<PropertyCollection, Property, Model.Property, IPropertyProvider, ISoftwareDataContext>
    {
        #region Constructors

        public PropertyCollection(ICollection<Property> list)
            : base(list)
        {
        }

        public PropertyCollection(IEnumerable<Model.Property> list)
            : base(list)
        {
        }

        public PropertyCollection()
            : base()
        {
        }

        #endregion Constructors

        #region Methods

        public static PropertyCollection GetByOwner(Guid id)
        {
            return new PropertyCollection(Provider.GetByOwner(id));
        }

        #endregion Methods
    }
}