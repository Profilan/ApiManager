
namespace ApiManager.Logic.Models
{
    public class ReceiveSendTask : SchedulerTask
    {
        public virtual TaskReceiver Receiver { get; set; }
        public virtual TaskSender Sender { get; set; }

        public ReceiveSendTask() { }
    }
}
