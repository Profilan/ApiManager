using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Mappings
{
    public class LogMap : ClassMap<Log>
    {
        public LogMap()
        {
            Table("EEK_API_LOG");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.TimeStamp).Column("date");
            Map(x => x.Priority).Column("type");
            Map(x => x.Message).Column("event");
            Map(x => x.PriorityName).Column("error_type");
            Map(x => x.Url);
            Map(x => x.Detail).Column("trace");
            Map(x => x.Acknowledged);
            Map(x => x.Duration);

            References(x => x.User).Column("userId").LazyLoad().Not.Cascade.SaveUpdate();
            References(x => x.Task).Column("taskId").LazyLoad().Not.Cascade.SaveUpdate();
        }
    }
}
