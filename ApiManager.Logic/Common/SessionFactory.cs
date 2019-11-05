using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Event;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace ApiManager.Logic.Common
{
    public class SessionFactory
    {
        private static readonly ISessionFactory _globalSessionFactory = new NHibernate.Cfg.Configuration().Configure().BuildSessionFactory();
        private static IDictionary<string, ISessionFactory> _allFactories = LoadAllFactories();

        private static IDictionary<string, ISessionFactory> LoadAllFactories()
        {
            var dictionary = new Dictionary<string, ISessionFactory>(2);

            // Database 100 (Exact)
            FluentConfiguration configuration = Fluently.Configure()
                           .Database(MsSqlConfiguration.MsSql2012.ConnectionString(cs => cs.FromConnectionStringWithKey("db1")))
                           .Mappings(m => m.FluentMappings
                                .AddFromAssembly(Assembly.Load("ApiManager.Logic"))
                                .Conventions.Add(
                                   ForeignKey.EndsWith("Id"),
                                   ConventionBuilder.Property
                                       .When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set), x => x.Not.Nullable()))

                           )
                           .ExposeConfiguration(x =>
                           {
                               x.EventListeners.PreInsertEventListeners =
                                   new IPreInsertEventListener[] { new EventListener() };
                               x.EventListeners.PreUpdateEventListeners =
                                   new IPreUpdateEventListener[] { new EventListener() };
                           });
            dictionary.Add("db1", configuration.BuildSessionFactory());

            // Database MAATWERK
            configuration = Fluently.Configure()
                           .Database(MsSqlConfiguration.MsSql2012.ConnectionString(cs => cs.FromConnectionStringWithKey("db2")))
                           .Mappings(m => m.FluentMappings
                                .AddFromAssembly(Assembly.Load("ApiManager.Logic"))
                                .Conventions.Add(
                                   ForeignKey.EndsWith("Id"),
                                   ConventionBuilder.Property
                                       .When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set), x => x.Not.Nullable()))

                           )
                           .ExposeConfiguration(x =>
                           {
                               x.EventListeners.PreInsertEventListeners =
                                   new IPreInsertEventListener[] { new EventListener() };
                               x.EventListeners.PreUpdateEventListeners =
                                   new IPreUpdateEventListener[] { new EventListener() };
                           });
            dictionary.Add("db2", configuration.BuildSessionFactory());

            return dictionary;
        }

        public static ISessionFactory GetSessionFactory(string identifier)
        {
            if (_allFactories == null)
            {
                _allFactories = LoadAllFactories();
            }

            return _allFactories[identifier];
        }

        public static ISession GetNewSession(string identifier)
        {
            return GetSessionFactory(identifier).OpenSession();
        }
    }
}
