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

        public object ListBySearchstring(string searchstring)
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
    }
}
