using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager.Logic.Models
{
    public class Role
    {
        public virtual Role Parent { get; set; }

    }
}
