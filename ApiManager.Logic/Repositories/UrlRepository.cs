using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Repositories
{
    public class UrlRepository : IRepository<Url, int>
    {
        public void Delete(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Load<Url>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public Url GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Get<Url>(id);

                return item;
            }
        }

        public Url GetByName(string name)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from u in session.Query<Url>()
                            select u;

                query = query.Where(u => u.Name == name);

                var users = query.ToList();

                if (users.Count > 0)
                {
                    return query.ToList().Last();
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<Url> ListBySearchstring(string searchstring)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from u in session.Query<Url>()
                            select u;

                query = query.Where(u => u.Name.Like("%" + searchstring + "%"));

                return query.ToList();
            }
        }

        public void Insert(Url entity)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<Url> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<Url>().OrderBy(x => x.Name);

                return query.ToList();
            }
        }

        public IEnumerable<Url> List(string sortOrder, string searchString)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Url>()
                            select l;

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(x => x.Name.Contains(searchString)
                                           || x.Address.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "hits.asc":
                        query = query.OrderBy(x => x.Hits);
                        break;
                    case "hits.desc":
                        query = query.OrderByDescending(x => x.Hits);
                        break;
                    case "address.asc":
                        query = query.OrderBy(x => x.Address);
                        break;
                    case "address.desc":
                        query = query.OrderByDescending(x => x.Address);
                        break;
                    case "name.desc":
                        query = query.OrderByDescending(x => x.Name);
                        break;
                    case "name.asc":
                    default:
                        query = query.OrderBy(x => x.Name);
                        break;
                }

                return query.ToList();
            }

        }
        public IEnumerable<Url> List(string sortOrder, string searchString, AccessType accessType)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Url>()
                            select l;

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(x => x.Name.Contains(searchString)
                                           || x.Address.Contains(searchString));
                }

                switch (accessType)
                {
                    case AccessType.Inbound:
                        query = query.Where(l => l.AccessType == accessType);
                        break;
                    case AccessType.Outbound:
                        query = query.Where(l => l.AccessType == accessType);
                        break;
                }

                switch (sortOrder)
                {
                    case "hits.asc":
                        query = query.OrderBy(x => x.Hits);
                        break;
                    case "hits.desc":
                        query = query.OrderByDescending(x => x.Hits);
                        break;
                    case "address.asc":
                        query = query.OrderBy(x => x.Address);
                        break;
                    case "address.desc":
                        query = query.OrderByDescending(x => x.Address);
                        break;
                    case "name.desc":
                        query = query.OrderByDescending(x => x.Name);
                        break;
                    case "name.asc":
                    default:
                        query = query.OrderBy(x => x.Name);
                        break;
                }

                return query.ToList();
            }

        }

        public IEnumerable<Url> ListTopFive()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<Url>().OrderByDescending(x => x.Hits).Take(5);

                return query.ToList();
            }
        }

        public void Update(Url entity)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            }
        }
        public IEnumerable<UrlStatistics> ListTopFive(Period period)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                return session.CreateSQLQuery("EXEC EEK_sp_API_TOP5_URLS @period=" + (int)period)
                                    .AddEntity(typeof(UrlStatistics))
                                    .List<UrlStatistics>();
            }
        }
    }
}
