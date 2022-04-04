using ApiManager.Logic.Common;
using System.Collections.Generic;

namespace ApiManager.Logic.Models
{
    public class Scheduler : Entity<int>
    {
        public virtual string HostName { get; set; }

        public virtual IList<SchedulerTask> Tasks { get; set; }
        public virtual bool Default { get; set; }

        public Scheduler()
        {
            Tasks = new List<SchedulerTask>();
        }
    }
}
