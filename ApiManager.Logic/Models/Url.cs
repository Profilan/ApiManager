using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Models
{
    public class Url : Entity<int>
    {

        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string ExternalUrl { get; set; }
        public virtual Interval InactivityTimeout { get; set; }
        public virtual bool MonitorInactivity { get; set; }
        public virtual int Hits { get; set; }
        public virtual bool ShowInStatistics { get; set; }
        public virtual AccessType AccessType { get; set; }

        public virtual ISet<User> Users { get; set; }

        public virtual Service Service { get; set; }

        public Url()
        {
            Users = new HashSet<User>();
        }
    }
}
