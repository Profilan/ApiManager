using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager.Logic.Mappings
{
    public class PasswordHashingMap : ClassMap<PasswordHashing>
    {
        public PasswordHashingMap()
        {
            Table("EEK_API_PASSWORD_HASHINGS");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Code);
        }
    }
}
