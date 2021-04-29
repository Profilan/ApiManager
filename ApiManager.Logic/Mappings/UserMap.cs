using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager.Logic.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("EEK_API_USERS");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Username);
            Map(x => x.DisplayName);
            Map(x => x.Email);
            Map(x => x.HashedPassword).Column("password");
            Map(x => x.RawPassword).Column("rawPassword");
            Map(x => x.Apikey);
            Map(x => x.Role);
            Map(x => x.AllowedIP).Column("ip_from");
            Map(x => x.Enabled);
            Map(x => x.SysCreated);
            Map(x => x.SysModified);

            References(x => x.Service).Column("ServiceId").LazyLoad().Not.Cascade.SaveUpdate();
 
            HasManyToMany(x => x.Debtors)
                .Table("EEK_API_WOOOD_USER_DEBTORS")
                .ParentKeyColumn("USER_ID")
                .ChildKeyColumn("DEBITEURNR")
                .Not.LazyLoad()
                .Cascade.None();

            HasManyToMany(x => x.Urls)
                .Table("EEK_API_USER_URLS")
                .ParentKeyColumn("user_id")
                .ChildKeyColumn("url_id")
                .Not.LazyLoad()
                .Cascade.SaveUpdate();
        }
    }
}
