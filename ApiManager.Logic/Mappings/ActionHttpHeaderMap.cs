using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Mappings
{
    public class ActionHttpHeaderMap : ClassMap<ActionHttpHeader>
    {
        public ActionHttpHeaderMap()
        {
            Table("EEK_API_ACTION_HEADERS");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.Value);

            References(x => x.Action, "ActionId");
        }
    }
}
