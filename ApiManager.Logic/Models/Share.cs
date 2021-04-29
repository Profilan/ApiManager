using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Models
{
    public class Share : Entity<int>
    {
        public virtual string Name { get; set; }
        public virtual string UNCPath { get; set; }

        public virtual Interval InactivityTimeout { get; set; }
        public virtual bool MonitorInactivity { get; set; }

        public virtual ISet<SchedulerTask> Tasks { get; set; }

        public virtual ISet<TaskReceiver> TaskActions { get; set; }

        public Share()
        {
            Tasks = new HashSet<SchedulerTask>();
        }
    }
}
