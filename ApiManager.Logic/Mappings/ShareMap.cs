using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Mappings
{
    public class ShareMap : ClassMap<Share>
    {
        public ShareMap()
        {
            Table("EEK_API_SHARES");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.UNCPath);
            Map(x => x.MonitorInactivity);

            Component(x => x.InactivityTimeout, m =>
            {
                m.Map(x => x.Seconds).Column("InactivityTimeout");
            });

            HasManyToMany(x => x.Tasks)
               .Table("EEK_API_TASK_SHARES")
               .ParentKeyColumn("ShareId")
               .ChildKeyColumn("TaskId")
               .Inverse()
               .Not.LazyLoad()
               .Cascade.SaveUpdate();
        }
    }
}
