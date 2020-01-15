using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Mappings
{
    public class UrlMap : ClassMap<Url>
    {
        public UrlMap()
        {
            Table("EEK_API_URLS");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.Address);
            Component(x => x.InactivityTimeout, m =>
            {
                m.Map(x => x.Seconds).Column("InactivityTimeout");
            });
            Map(x => x.MonitorInactivity);
            Map(x => x.Hits);
            Map(x => x.ShowInStatistics);

            References(x => x.Service).Column("ServiceId").LazyLoad().Not.Cascade.SaveUpdate();
        }
    }
}
