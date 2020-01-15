using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Repositories
{
    public class ServiceRepository : IRepository<Service, int>
    {
        public void Delete(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Load<Service>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public Service GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Get<Service>(id);

                return item;
            }
        }

        public void Insert(Service entity)
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

        public IEnumerable<Service> List(string sortOrder, string searchString)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Service>()
                            select l;

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(x => x.Code.Contains(searchString)
                                           || x.ExternalUrl.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "code.asc":
                        query = query.OrderBy(x => x.Code);
                        break;
                    case "code.desc":
                        query = query.OrderByDescending(x => x.Code);
                        break;
                    default:
                        query = query.OrderBy(x => x.Code);
                        break;
                }

                return query.ToList();
            }
        }

        public IEnumerable<Service> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<Service>().OrderBy(x => x.Code);

                return query.ToList();
            }
        }

        public void Update(Service entity)
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
