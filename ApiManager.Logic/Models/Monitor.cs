using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Models
{
    public class Monitor : Entity<Guid>
    {
        public virtual string Name { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual ISet<Messenger> Messengers { get; set; }

        public Monitor() : base(Guid.NewGuid())
        {
            Messengers = new HashSet<Messenger>();
        }
    }
}
