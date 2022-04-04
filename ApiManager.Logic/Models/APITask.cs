using ApiManager.Logic.Common;

namespace ApiManager.Logic.Models
{
    public class APITask : SchedulerTask
    {
        protected APITask()
        {

        }

        public APITask(string title,
            Authentication authentication,
            bool enabled,
            int totalProcesseditems = 100
            ) : base(title, authentication, enabled, totalProcesseditems)
        {

        }
    }
}
