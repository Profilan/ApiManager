
namespace ApiManager.Logic.Models
{
    public class RECEIVESENDTask : SchedulerTask
    {
        protected RECEIVESENDTask()
        {

        }

        public RECEIVESENDTask(string title,
            Authentication authentication,
            bool enabled,
            int totalProcesseditems = 100
            ) : base(title, authentication, enabled, totalProcesseditems)
        {

        }
    }
}
