using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager.Logic.Mappings
{
    public class ServiceMap : ClassMap<Service>
    {
        public ServiceMap()
        {
            Table("EEK_API_SERVICES");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Code);
        }
    }
}
