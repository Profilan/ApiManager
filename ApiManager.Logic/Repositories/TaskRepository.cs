using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiManager.Logic.Repositories
{
    public class TaskRepository : IRepository<SchedulerTask, Guid>
    {
        public void Delete(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Load<SchedulerTask>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<SchedulerTask> List(string sortOrder, string searchString)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<SchedulerTask>()
                            select l;

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(x => x.Title.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "title.desc":
                        query = query.OrderByDescending(x => x.Title);
                        break;
                    case "title.asc":
                    default:
                        query = query.OrderBy(x => x.Title);
                        break;
                }

                return query.ToList();
            }
        }

        public SchedulerTask GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var task = session.Get<SchedulerTask>(id);
                if (task != null)
                {
                    NHibernateUtil.Initialize(task.HttpHeaders);
                    NHibernateUtil.Initialize(task.Shares);
                    NHibernateUtil.Initialize(task.Url);

                    if (task.GetType() == typeof(ReceiveSendTask))
                    {
                        NHibernateUtil.Initialize(((ReceiveSendTask)task).Receiver);
                        NHibernateUtil.Initialize(((ReceiveSendTask)task).Sender);
                    }
                }
                return task;
            }
        }

        public void Insert(SchedulerTask entity)
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

        public IEnumerable<SchedulerTask> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<SchedulerTask>()
                    .FetchMany(x => x.HttpHeaders)
                    .OrderBy(x => x.Title);

                return query.ToList();

            }
        }

        public void Update(SchedulerTask entity)
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

        public IEnumerable<SchedulerTask> Reset(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var result = session.CreateSQLQuery("exec EEK_sp_API_TASK_RESET :pId")
                    .AddEntity(typeof(SchedulerTask))
                    .SetParameter("pId", id)
                    .List<SchedulerTask>();

                return result;
            }
        }
    }
}
