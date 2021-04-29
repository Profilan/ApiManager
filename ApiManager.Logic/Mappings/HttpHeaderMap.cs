using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Mappings
{
    public class HttpHeaderMap : ClassMap<HttpHeader>
    {
        public HttpHeaderMap()
        {
            Table("EEK_API_TASK_HEADERS");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.Value);

            References(x => x.Task, "TaskId");
        }
    }
}
