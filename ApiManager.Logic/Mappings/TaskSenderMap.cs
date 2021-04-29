
using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;

namespace ApiManager.Logic.Mappings
{
    public class TaskSenderMap : SubclassMap<TaskSender>
    {
        public TaskSenderMap()
        {
            DiscriminatorValue("SENDER");
        }
    }
}
