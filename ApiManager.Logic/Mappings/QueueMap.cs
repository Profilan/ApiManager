using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;

namespace ApiManager.Logic.Mappings
{
    public class QueueMap : ClassMap<Queue>
    {
        public QueueMap()
        {
            Table("EEK_API_QUEUE");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Key).Column("`key`");
            Map(x => x.TryCount);
            Map(x => x.SysCreated);

            References(x => x.Task).Column("TaskId").LazyLoad().Not.Cascade.SaveUpdate();
        }
    }
}
