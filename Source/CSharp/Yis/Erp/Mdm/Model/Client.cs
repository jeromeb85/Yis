﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Model;

namespace Yis.Erp.Mdm.Model
{
    public class Client :  ModelBase
    {

        public Guid Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
    }
}
