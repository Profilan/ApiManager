using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Models
{
    public class Messenger : Entity<Guid>
    {
        public virtual string Name { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual ISet<Monitor> Monitors { get; set; }

        public Messenger() : base(Guid.NewGuid())
        {
            Enabled = false;

            Monitors = new HashSet<Monitor>();
        }
    }
}
