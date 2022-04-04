using ApiManager.Logic.Common;
using System;

namespace ApiManager.Logic.Models
{
    public class Queue : Entity<int>
    {
        public virtual int Key { get; set; }
        public virtual int TryCount { get; set; }
        public virtual DateTime SysCreated { get; set; }
        public virtual SchedulerTask Task { get; set; }
        public virtual string Error { get; set; }

        protected Queue()
        {
            SysCreated = DateTime.Now;
        }

        public Queue(int key,
            int tryCount,
            SchedulerTask task) : this()
        {
            Key = key;
            TryCount = tryCount;
            Task = Task;
        }
    }
}
