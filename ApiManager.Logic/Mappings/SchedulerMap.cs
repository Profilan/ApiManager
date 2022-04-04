using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;

namespace ApiManager.Logic.Mappings
{
    public class SchedulerMap : ClassMap<Scheduler>
    {
        public SchedulerMap()
        {
            Table("EEK_API_SCHEDULERS");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.HostName);
            Map(x => x.Default).Column("DefaultHost");

            HasMany(x => x.Tasks)
                .Table("EEK_API_TASK")
                .KeyColumn("ScheduleId")
                // .Inverse()
                .Not.LazyLoad()
                .Cascade.SaveUpdate();

        }
    }
}
