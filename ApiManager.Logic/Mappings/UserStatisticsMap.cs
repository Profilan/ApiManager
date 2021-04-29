using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Mappings
{
    public class UserStatisticsMap : ClassMap<UserStatistics>
    {
        public UserStatisticsMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Username);
            Map(x => x.Total);
        }
    }
}
