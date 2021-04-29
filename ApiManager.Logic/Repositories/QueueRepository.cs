using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiManager.Logic.Repositories
{
    public class QueueRepository : IRepository<Queue, int>
    {
        public void Delete(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Load<Queue>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public Queue GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Queue entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Queue> ListByTaskAndCount(Guid taskId, int count)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Queue>()
                            select l;

                query = query.Where(l => l.Task.Id == taskId)
                    .Where(l => l.TryCount <= l.Task.MaxErrors).Take(count);

                return query.ToList();
            }
        }

        public IEnumerable<Queue> ListByTask(Guid taskId)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<Queue>()
                    .Fetch(x => x.Task)
                    .ToFuture()
                    .Where(x => x.Task.Id == taskId)
                    .OrderBy(x => x.Id);

                return query.ToList();
            }
        }

        public IEnumerable<Queue> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<Queue>()
                    .Fetch(x => x.Task)
                    .ToFuture()
                    .OrderBy(x => x.Id);


                return query.ToList();
            }
        }

        public IEnumerable<Queue> ListTasksBeforeDate(Guid taskId, DateTime sysCreated)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Queue>()
                            select l;

                query = query.Where(l => l.Task.Id == taskId)
                    .Where(l => l.SysCreated <= sysCreated);

                return query.ToList();
            }
        }


        public void Update(Queue entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
