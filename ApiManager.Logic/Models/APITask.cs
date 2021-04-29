using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Models
{
    public class APITask : SchedulerTask
    {
        protected APITask()
        {

        }

        public APITask(string title,
            Schedule schedule,
            Authentication authentication,
            bool enabled,
            int totalProcesseditems = 100
            ) : base(title, schedule, authentication, enabled, totalProcesseditems)
        {

        }
    }
}
