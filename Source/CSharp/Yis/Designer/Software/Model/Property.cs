using System;
using Yis.Framework.Model;

namespace Yis.Designer.Software.Model
{
    public class Property : ModelBase
    {
        #region Properties

        public Guid OwnerId { get; set; }

        public string Comment { get; set; }

        public Guid Id { get; set; }

        public string GetCode { get; set; }
        public string SetCode { get; set; }

        public string Name { get; set; }

        public Scope Scope { get; set; }

        public string Type { get; set; }

        #endregion Properties
    }
}