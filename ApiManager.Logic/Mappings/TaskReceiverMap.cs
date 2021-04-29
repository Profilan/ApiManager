
using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;

namespace ApiManager.Logic.Mappings
{
    public class TaskReceiverMap : SubclassMap<TaskReceiver>
    {
        public TaskReceiverMap()
        {

            DiscriminatorValue("RECEIVER");

        }
    }
}
