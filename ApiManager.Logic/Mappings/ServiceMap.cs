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

            References(x => x.PasswordHashing).Column("PasswordHashingId").LazyLoad().Not.Cascade.SaveUpdate();

            Map(x => x.Code);
            Map(x => x.ExternalUrl).Nullable();
            Map(x => x.HTPasswordLocation).Nullable();
        }
    }
}
