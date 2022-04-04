using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Models
{
    public class FILETask : SchedulerTask
    {
        protected FILETask()
        {

        }

        public FILETask(string title,
            Authentication authentication,
            bool enabled
            ) : base(title, authentication, enabled)
        {

        }
    }
}
