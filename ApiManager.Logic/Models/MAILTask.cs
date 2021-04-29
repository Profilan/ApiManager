using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Models
{
    public class MAILTask : SchedulerTask
    {
        protected MAILTask()
        {

        }

        public MAILTask(string title,
            Schedule schedule,
            Authentication authentication,
            bool enabled
            ) : base(title, schedule, authentication, enabled)
        {

        }
    }
}
