using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiManager.Logic.Models
{
    public class SchedulerTask : Entity<Guid>
    {
        public virtual string Title { get; set; }
        public virtual TaskType TaskType { get; set; }
        public virtual int ScheduleId { get; set; }
        public virtual Scheduler Scheduler { get; set; }

        public virtual Status Status { get; protected set; }

        // public virtual Schedule Schedule { get; set; }

        public virtual Authentication Authentication { get; set; }

        public virtual Url Url { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual string QueueName { get; set; }
        public virtual HttpMethod HttpMethod { get; set; }

        public virtual ApiType ApiType { get; set; }
        public virtual GraphQLMethod GraphQLMethod { get; set; }

        public virtual string ContentFormats { get; set; }
        public virtual string Classname { get; set; }
        public virtual string SPLogger { get; set; }

        public virtual string LastRunResult { get; set; }
        public virtual DateTime LastRunTime { get; set; }
        public virtual string LastRunDetails { get; set; }

        public virtual string MailSender { get; set; }
        public virtual string MailRecipient { get; set; }

        public virtual int MaxErrors { get; set; }
        public virtual bool Active { get; set; }
        public virtual int TotalProcessedItems { get; set; }

        public virtual ISet<Share> Shares { get; set; }
        public virtual ISet<HttpHeader> HttpHeaders { get; set; }
        public virtual ISet<Schedule> Schedules { get; set; }

        public SchedulerTask()
        {
            Shares = new HashSet<Share>();
            HttpHeaders = new HashSet<HttpHeader>();
            Schedules = new HashSet<Schedule>();
        }

        public SchedulerTask(string title,
            Authentication authentication,
            bool enabled,
            int totalProcessedItems = 100
            ) : base(Guid.NewGuid())
        {
            Active = false;

            Title = title;
            ScheduleId = 1;
            Authentication = authentication;
            Enabled = enabled;
            LastRunTime = DateTime.Now;
            LastRunResult = "0 Not Run";
            MaxErrors = 4;
            TotalProcessedItems = totalProcessedItems;

            Shares = new HashSet<Share>();
            HttpHeaders = new HashSet<HttpHeader>();
            Schedules = new HashSet<Schedule>();
        }
        public virtual bool AddHeader(HttpHeader newHeader)
        {
            if (newHeader != null && HttpHeaders.Add(newHeader))
            {
                newHeader.SetTask(this);
                return true;
            }
            return false;
        }

        public virtual bool RemoveHeader(HttpHeader headerToRemove)
        {
            if (headerToRemove != null && HttpHeaders.Remove(headerToRemove))
            {
                headerToRemove.SetTask(null);
                return true;
            }
            return false;
        }
        public virtual bool AddSchedule(Schedule newSchedule)
        {
            if (newSchedule != null && Schedules.Add(newSchedule))
            {
                newSchedule.SetTask(this);
                return true;
            }
            return false;
        }

        public virtual bool RemoveSchedule(Schedule scheduleToRemove)
        {
            if (scheduleToRemove != null && Schedules.Remove(scheduleToRemove))
            {
                scheduleToRemove.SetTask(null);
                return true;
            }
            return false;
        }
    }
}


