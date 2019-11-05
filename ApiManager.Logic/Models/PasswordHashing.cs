using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager.Logic.Models
{
    public class PasswordHashing : Entity<int>
    {
        public virtual string Code { get; set; }

        protected PasswordHashing() { }

        public PasswordHashing(string code)
        {
            Code = code;
        }
    }
}
