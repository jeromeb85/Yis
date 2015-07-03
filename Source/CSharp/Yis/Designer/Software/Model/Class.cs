using System;
using System.Collections.Generic;

namespace Yis.Designer.Software.Model
{
    public class Class : Classifier
    {
        #region Properties

        public string BaseType { get; set; }

        public Guid Id { get; set; }

        public IList<string> Implement { get; set; }

        public IList<string> Import { get; set; }

        public bool IsInterface { get; set; }

        public string Name { get; set; }

        public Guid NameSpaceId { get; set; }

        public Scope Scope { get; set; }

        #endregion Properties
    }
}