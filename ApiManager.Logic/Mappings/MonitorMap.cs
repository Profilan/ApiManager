using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Mappings
{
    public class MonitorMap : ClassMap<Monitor>
    {
        public MonitorMap()
        {
            Table("EEK_API_MONITORS");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.Name);
            Map(x => x.Enabled);

            HasManyToMany(x => x.Messengers)
                .Table("EEK_API_MONITOR_MESSENGERS")
                .ParentKeyColumn("MonitorId")
                .ChildKeyColumn("MessengerId")
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan();
        }
    }
}
