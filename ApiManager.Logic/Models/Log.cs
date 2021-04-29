using ApiManager.Logic.Common;
using System;

namespace ApiManager.Logic.Models
{
    public class Log : Entity<int>
    {
        public virtual DateTime TimeStamp { get; protected set; }
        public virtual int Priority { get; protected set; }
        public virtual string Message { get; protected set; }
        public virtual string PriorityName { get; protected set; }
        public virtual string Url { get; protected set; }
        public virtual string Detail { get; protected set; }
        public virtual bool Acknowledged { get; set; }
        public virtual double Duration { get; set; }

        public virtual User User { get; set; }
         public virtual SchedulerTask Task { get; set; }

        // Needed for NHibernate
        public Log()
        {

        }

        public Log(DateTime timeStamp,
            int priority,
            string message,
            string priorityName,
            string url,
            string detail,
            bool acknowledged,
            User user,
            SchedulerTask task,
            double duration = 0
            )
        {
            TimeStamp = timeStamp;
            Priority = priority;
            Message = message;
            PriorityName = priorityName;
            Url = url;
            Detail = detail;
            Acknowledged = acknowledged;
            User = user;
            Duration = duration;
            Task = task;
        }

        // requestId, targetId, response, properties, referenceId
    }
}
