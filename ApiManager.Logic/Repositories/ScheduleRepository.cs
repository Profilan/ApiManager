using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiManager.Logic.Repositories
{
    public class ScheduleRepository : IRepository<Schedule, Guid>
    {
        public void Delete(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Load<Schedule>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<Schedule> ListByTask(Guid taskId, string sortOrder)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Schedule>()
                            select l;

                // TODO: Manage order
                //switch (sortOrder)
                //{
                //    case "created.asc":
                //        query = query.OrderBy(x => x.Created);
                //        break;
                //    case "created.desc":
                //        query = query.OrderByDescending(x => x.Created);
                //        break;
                //    case "type.asc":
                //        query = query.OrderBy(x => x.Type);
                //        break;
                //    case "type.desc":
                //        query = query.OrderByDescending(x => x.Type);
                //        break;
                //    default:
                //        query = query.OrderByDescending(x => x.Created);
                //        break;
                //}

                query = query.Where(x => x.Task.Id == taskId);

                return query.ToList();
            }
        }

        public Schedule GetById(Guid id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var item = session.Get<Schedule>(id);
                    NHibernateUtil.Initialize(item.Task);
                    return item;
                }
            }
        }

        public void Insert(Schedule entity)
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

        public IEnumerable<Schedule> List(string sortOrder, string searchString)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Schedule>()
                            select l;

                //switch (sortOrder)
                //{
                //    case "created.asc":
                //        query = query.OrderBy(x => x.Created);
                //        break;
                //    case "created.desc":
                //        query = query.OrderByDescending(x => x.Created);
                //        break;
                //    case "type.asc":
                //        query = query.OrderBy(x => x.Type);
                //        break;
                //    case "type.desc":
                //        query = query.OrderByDescending(x => x.Type);
                //        break;
                //    default:
                //        query = query.OrderByDescending(x => x.Created);
                //        break;
                //}

                return query.ToList();
            }
        }

        public IEnumerable<Schedule> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Schedule>()
                            select l;

                // query = query.OrderByDescending(l => l.Created);

                return query.ToList();
            }
        }

        public void Update(Schedule entity)
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
