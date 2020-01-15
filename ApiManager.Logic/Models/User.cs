using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager.Logic.Models
{
    public class User : Entity<int>
    {
        public virtual string Username { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Email { get; set; }
        public virtual string HashedPassword { get; set; }
        public virtual string RawPassword { get; set; }
        public virtual string Apikey { get; set; }
        public virtual string Debcode { get; set; }
        public virtual string Role { get; set; }
        public virtual string AllowedIP { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual int State { get; set; }
        public virtual string Description { get; set; }
        public virtual Service Service { get; set; }
         public virtual DateTime SysCreated { get; set; }
        public virtual int SysCreator { get; set; }
        public virtual DateTime SysModified { get; set; }
        public virtual int SysModifier { get; set; }

        public virtual ISet<Debtor> Debtors { get; set; }
        public virtual ISet<Url> Urls { get; set; }

        public User()
        {
            Debtors = new HashSet<Debtor>();
            Urls = new HashSet<Url>();
        }
    }
}
