using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager.Logic.Models
{
    public class Service : Entity<int>
    {
        public virtual string Code { get; set; }
        public virtual PasswordHashing PasswordHashing { get; set; }
        public virtual string ExternalUrl { get; set; }
        public virtual string HTPasswordLocation { get; set; }

        protected Service() { }

        public Service(string code)
        {
            Code = code;
        }
    }
}
