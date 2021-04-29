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
    public class ShareRepository : IRepository<Share, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Share GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var share = session.Get<Share>(id);
                return share;
            }
        }

        public void Insert(Share entity)
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

        public IEnumerable<Share> List(string sortOrder, string searchString)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Share>()
                            select l;

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(x => x.Name.Contains(searchString)
                                           || x.UNCPath.Contains(searchString));
                }

                switch (sortOrder)
                {
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

        public IEnumerable<Share> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from p in session.Query<Share>()
                            select p;

                return query.ToList();
            }
        }

        public void Update(Share entity)
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
