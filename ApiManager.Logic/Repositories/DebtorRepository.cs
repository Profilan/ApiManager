using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiManager.Logic.Repositories
{
    public class DebtorRepository : IRepository<Debtor, string>
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Debtor GetById(string id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Get<Debtor>(id);

                return item;
            }
        }

        public void Insert(Debtor entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Debtor> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<Debtor>();

                query = query.OrderBy(x => x.DEBITEURNR);

                return query.ToList();
            }
        }

        public IEnumerable<Debtor> ListById(string id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from d in session.Query<Debtor>()
                            select d;

                query = query.Where(d => d.DEBITEURNR == id);

                return query.ToList();
            }
        }

        public IEnumerable<Debtor> ListBySearchstring(string searchstring)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from d in session.Query<Debtor>()
                            select d;

                query = query.Where(d => d.NAAM.Like("%" + searchstring + "%") || d.DEBITEURNR.Like("%" + searchstring + "%"));

                return query.ToList();
            }
        }

        public void Update(Debtor entity)
        {
            throw new NotImplementedException();
        }
    }
}
