﻿using System;
using Yis.Framework.Model;

namespace Yis.Designer.Software.Model
{
    public class NameSpace : ModelBase
    {
        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ParentNameSpaceId { get; set; }

        #endregion Properties
    }
}