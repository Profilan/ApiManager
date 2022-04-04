using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Repositories
{
    public class LogRepository : IRepository<Log, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Log GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Get<Log>(id);

                return item;
            }
        }

        public void Insert(Log entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Log> List()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Log> List(string searchString, int page, int length, DateTime startDate, DateTime endDate, string sortOrder = "TimeStamp.desc", int userId = -1, ErrorType errorType = ErrorType.NONE, Guid taskId = new Guid())
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var items = session.CreateCriteria<Log>()
                    .SetMaxResults(length)
                    .SetFirstResult((page - 1) * length);

                items.Add(Expression.Ge("TimeStamp", startDate));
                items.Add(Expression.Le("TimeStamp", endDate));

                if (!String.IsNullOrEmpty(searchString))
                {
                    items.Add(Expression.Disjunction()
                        .Add(Expression.Like("Url", "%" + searchString + "%"))
                        .Add(Expression.Like("Message", "%" + searchString + "%"))
                        .Add(Expression.Like("Detail", "%" + searchString + "%"))
                        .Add(Expression.Like("PriorityName", "%" + searchString + "%"))
                        );
                }

                if (taskId != Guid.Empty)
                {
                    items.Add(Expression.Eq("Task.Id", taskId));
                }

                if (errorType != ErrorType.NONE)
                {
                    items.Add(Expression.Eq("PriorityName", errorType.ToString()));
                }

                if (userId != -1)
                {
                    items.Add(Expression.Eq("User.Id", userId));
                }

                var sort = sortOrder.Split('.');
                if (sort[1] == "desc")
                {
                    items.AddOrder(Order.Desc(sort[0]));
                }
                else
                {
                    items.AddOrder(Order.Asc(sort[0]));
                }
 
                return items.List<Log>();
            }
        }

        public int GetTotal(string searchString, DateTime startDate, DateTime endDate, int userId = -1, ErrorType errorType = ErrorType.NONE, Guid taskId = new Guid())
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var items = session.CreateCriteria<Log>()
                                .SetProjection(
                                    Projections.Count(Projections.Id())
                                );

                items.Add(Expression.Ge("TimeStamp", startDate));
                items.Add(Expression.Le("TimeStamp", endDate));

                if (!String.IsNullOrEmpty(searchString))
                {
                    items.Add(Expression.Disjunction()
                        .Add(Expression.Like("Url", "%" + searchString + "%"))
                        .Add(Expression.Like("Message", "%" + searchString + "%"))
                        .Add(Expression.Like("Detail", "%" + searchString + "%"))
                        .Add(Expression.Like("PriorityName", "%" + searchString + "%"))
                        );
                }

                if (errorType != ErrorType.NONE)
                {
                    items.Add(Expression.Eq("PriorityName", errorType.ToString()));
                }

                if (userId != -1)
                {
                    items.Add(Expression.Eq("User.Id", userId));
                }

                if (taskId != Guid.Empty)
                {
                    items.Add(Expression.Eq("Task.Id", taskId));
                }

                return items.UniqueResult<int>();
            }

        }

        public IEnumerable<Log> ListByTypeAndTask(Period period, ErrorType errorType, SchedulerTask task, bool acknowledged = false)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Log>()
                            select l;

                var dateNow = DateTime.Now;
                var dateBefore = dateNow.AddDays(-30);

                switch (period)
                {
                    case Period.Hour:
                        dateBefore = dateNow.AddHours(-1);
                        break;
                    case Period.Day:
                        dateBefore = dateNow.AddDays(-1);
                        break;
                    case Period.Week:
                        dateBefore = dateNow.AddDays(-7);
                        break;
                }
                query = query.Where(l => l.TimeStamp >= dateBefore && l.TimeStamp <= dateNow);
                query = query.Where(l => l.Acknowledged == acknowledged);
                query = query.Where(l => l.Priority == Convert.ToInt32(errorType));
                query = query.Where(l => l.Task == task);

                return query.ToList();
            }
        }

        public IEnumerable<Log> ListByTypeAndUrl(Period period, ErrorType errorType, Url url, bool acknowledged = false)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Log>()
                            select l;

                var dateNow = DateTime.Now;
                var dateBefore = dateNow.AddDays(-30);

                switch (period)
                {
                    case Period.Hour:
                        dateBefore = dateNow.AddHours(-1);
                        break;
                    case Period.Day:
                        dateBefore = dateNow.AddDays(-1);
                        break;
                    case Period.Week:
                        dateBefore = dateNow.AddDays(-7);
                        break;
                }
                query = query.Where(l => l.TimeStamp >= dateBefore && l.TimeStamp <= dateNow);
                query = query.Where(l => l.Acknowledged == acknowledged);
                query = query.Where(l => l.Priority == Convert.ToInt32(errorType));
                query = query.Where(l => l.Url == url.Name);

                return query.ToList();
            }
        }

        public void Update(Log entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Log> ListByUserAndUrl(User user, Url url, Period period)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Log>()
                            select l;


                var dateNow = DateTime.Now;
                var dateBefore = dateNow.AddDays(-30);

                switch (period)
                {
                    case Period.Hour:
                        dateBefore = dateNow.AddHours(-1);
                        break;
                    case Period.Day:
                        dateBefore = dateNow.AddDays(-1);
                        break;
                    case Period.Week:
                        dateBefore = dateNow.AddDays(-7);
                        break;
                }
                query = query.Where(l => l.TimeStamp >= dateBefore);
                query = query.Where(l => l.TimeStamp <= dateNow);
                query = query.Where(l => l.User.Id == user.Id);
                query = query.Where(l => l.Url == url.Name);

                query.OrderByDescending(l => l.TimeStamp);

                return query.ToList();
            }
        }
    }
}
