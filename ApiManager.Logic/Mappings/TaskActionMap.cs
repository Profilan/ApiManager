
using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System.Security.Cryptography.X509Certificates;

namespace ApiManager.Logic.Mappings
{
    public class TaskActionMap : ClassMap<TaskAction>
    {
        public TaskActionMap()
        {
            Table("EEK_API_TASK_ACTIONS");

            DiscriminateSubClassesOnColumn("Type").Not.Nullable();

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.ActionType).CustomType(typeof(ActionType));
            Map(x => x.ApiType).CustomType(typeof(ActionType));
            Map(x => x.ContentFormats).Nullable();
            Map(x => x.HttpMethod).CustomType(typeof(HttpMethod));
            Map(x => x.GraphQLMethod).CustomType(typeof(GraphQLMethod));
            Map(x => x.MailSender).Nullable();
            Map(x => x.MailRecipient).Nullable();

            Component(x => x.Authentication, m =>
            {
                m.Map(x => x.Username).Nullable();
                m.Map(x => x.Password).Nullable();
                m.Map(x => x.AuthenticationType).CustomType(typeof(AuthenticationType));
                m.Map(x => x.Scope).Nullable();
                m.Map(x => x.GrantType).Nullable();
                m.Map(x => x.OAuthUrl).Nullable();
                m.Map(x => x.OAuthAudience).Nullable();
                m.Map(x => x.ApiKey).Nullable();
            });

            Component(x => x.Status, m =>
            {
                m.Map(x => x.Code).Column("StatusCode").Nullable();
                m.Map(x => x.Description).Column("StatusDescription").Nullable();
            });

            References(x => x.Url).Column("UrlId");
            References(x => x.Task).Column("TaskId");

            HasMany(x => x.HttpHeaders)
                .Table("EEK_API_ACTION_HEADERS")
                .KeyColumn("ActionId")
                // .Inverse()
                .Not.LazyLoad()
                .Cascade.SaveUpdate();

            HasManyToMany(x => x.Shares)
                .Table("EEK_API_ACTION_SHARES")
                .ParentKeyColumn("ActionId")
                .ChildKeyColumn("ShareId")
                .Not.LazyLoad()
                .Cascade.SaveUpdate();
        }
    }
}
