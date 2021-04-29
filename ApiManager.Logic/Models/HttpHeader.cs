
using System;

namespace ApiManager.Logic.Models
{
    public class HttpHeader
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }

        public virtual SchedulerTask Task { get; set; }

        public HttpHeader() { }

        public HttpHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public virtual void SetTask(SchedulerTask newTask)
        {
            var prevTask = Task;

            if (newTask == prevTask)
                return;

            Task = newTask;

            if (prevTask != null)
                prevTask.RemoveHeader(this);

            if (newTask != null)
                newTask.AddHeader(this);

        }
    }
}
