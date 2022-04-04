using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace ApiManager.Logic.Repositories
{
    public class SchedulerRepository : IRepository<Scheduler, int>
    {
        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Scheduler GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Scheduler entity)
        {
            throw new System.NotImplementedException();
        }


        public IEnumerable<Scheduler> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<Scheduler>()
                    .OrderBy(x => x.HostName);

                return query.ToList();

            }
        }

        public void Update(Scheduler entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
