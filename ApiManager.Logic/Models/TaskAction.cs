
using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;

namespace ApiManager.Logic.Models
{
    public abstract class TaskAction : Entity<Guid>
    {
        public virtual ActionType ActionType { get; set; }
        public virtual ApiType ApiType { get; set; }
        public virtual string ContentFormats { get; set; }
        public virtual Authentication Authentication { get; set; }
        public virtual Status Status { get; set; }
        public virtual HttpMethod HttpMethod { get; set; }
        public virtual GraphQLMethod GraphQLMethod { get; set; }
        public virtual Url Url { get; set; }
        public virtual ISet<ActionHttpHeader> HttpHeaders { get; set; }
        public virtual ISet<Share> Shares { get; set; }
        public virtual string MailSender { get; set; }
        public virtual string MailRecipient { get; set; }
        public virtual SchedulerTask Task { get; set; }

        public TaskAction()
        {
            HttpHeaders = new HashSet<ActionHttpHeader>();
            Shares = new HashSet<Share>();
        }
    }

}
