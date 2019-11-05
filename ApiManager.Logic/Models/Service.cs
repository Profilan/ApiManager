using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager.Logic.Models
{
    public class Service : Entity<int>
    {
        public virtual string Code { get; set; }

        protected Service() { }

        public Service(string code)
        {
            Code = code;
        }
    }
}
