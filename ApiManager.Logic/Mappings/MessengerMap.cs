using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Mappings
{
    public class MessengerMap : ClassMap<Messenger>
    {
        public MessengerMap()
        {
            Table("EEK_API_MESSENGERS");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.Name);
            Map(x => x.Enabled);

            HasManyToMany(x => x.Monitors)
                .Table("EEK_API_MONITOR_MESSENGERS")
                .Inverse()
                .ParentKeyColumn("MessengerId")
                .ChildKeyColumn("MonitorId")
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan();
        }
    }
}
