using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;

namespace ApiManager.Logic.Mappings
{
    public class SchedulerTaskMap : ClassMap<SchedulerTask>
    {
        public SchedulerTaskMap()
        {
            Table("EEK_API_TASK");

            Id(x => x.Id).GeneratedBy.GuidComb();

            DiscriminateSubClassesOnColumn("Type").Not.Nullable();

            Map(x => x.Title);
            Map(x => x.Classname).Column("DiskClassname");
            Map(x => x.SPLogger).Nullable();
            Map(x => x.ContentFormats).Column("DiskContentFormats").Nullable();
            Map(x => x.MaxErrors);
            Map(x => x.TaskType).CustomType(typeof(TaskType));
            Map(x => x.Enabled);
            Map(x => x.LastRunTime).Nullable();
            Map(x => x.LastRunResult).Nullable();
            Map(x => x.LastRunDetails).Nullable();
            Map(x => x.TotalProcessedItems).Nullable();
            Map(x => x.Active).Nullable();

            References(x => x.Scheduler).Column("ScheduleId");

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


            HasMany(x => x.Schedules)
                .Table("EEK_API_SCHEDULES")
                .KeyColumn("TaskId")
                // .Inverse()
                .Not.LazyLoad()
                .Cascade.SaveUpdate();

        }
    }

    public class APITaskMap : SubclassMap<APITask>
    {
        public APITaskMap()
        {
            DiscriminatorValue("API");

            Map(x => x.ApiType).CustomType(typeof(ActionType));
            Map(x => x.QueueName).Nullable();
            Map(x => x.HttpMethod).CustomType(typeof(HttpMethod));

            References(x => x.Url).Column("UrlId");

            HasMany(x => x.HttpHeaders)
                .Table("EEK_API_TASK_HEADERS")
                .KeyColumn("TaskId")
                // .Inverse()
                .Not.LazyLoad()
                .Cascade.SaveUpdate();
        }
    }

    public class RECEIVESENDTaskMap : SubclassMap<RECEIVESENDTask>
    {
        public RECEIVESENDTaskMap()
        {
            DiscriminatorValue("RECEIVESEND");

            Map(x => x.ApiType).CustomType(typeof(ActionType));
            Map(x => x.QueueName).Nullable();
            Map(x => x.GraphQLMethod).CustomType(typeof(GraphQLMethod));

            References(x => x.Url).Column("UrlId");

            HasMany(x => x.HttpHeaders)
                .Table("EEK_API_TASK_HEADERS")
                .KeyColumn("TaskId")
                // .Inverse()
                .Not.LazyLoad()
                .Cascade.SaveUpdate();

            HasManyToMany(x => x.Shares)
                .Table("EEK_API_TASK_SHARES")
                .ParentKeyColumn("TaskId")
                .ChildKeyColumn("ShareId")
                .Not.LazyLoad()
                .Cascade.SaveUpdate();
        }
    }

    public class MAILTaskMap : SubclassMap<MAILTask>
    {
        public MAILTaskMap()
        {
            DiscriminatorValue("MAIL");

            Map(x => x.MailSender).Nullable();
            Map(x => x.MailRecipient).Nullable();
        }
    }

    public class FILETaskMap : SubclassMap<FILETask>
    { 
        public FILETaskMap()
        {
            DiscriminatorValue("FILE");

            HasManyToMany(x => x.Shares)
                .Table("EEK_API_TASK_SHARES")
                .ParentKeyColumn("TaskId")
                .ChildKeyColumn("ShareId")
                .Not.LazyLoad()
                .Cascade.SaveUpdate();
        }
    }

}
