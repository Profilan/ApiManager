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
        public IEnumerable<Log> List(string sortOrder, string searchString, DateTime startDate, DateTime endDate, int userId = -1, ErrorType errorType = ErrorType.NONE)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Log>()
                            select l;

                // query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(x => x.Url.Like("%" + searchString + "%")
                        || x.Message.Like("%" + searchString + "%")
                        || x.Detail.Like("%" + searchString + "%")
                        || x.PriorityName.Like("%" + searchString + "%"));
                }

                query = query.Where(l => l.TimeStamp >= startDate && l.TimeStamp <= endDate);

                if (errorType != ErrorType.NONE)
                {
                    query = query.Where(l => l.PriorityName == errorType.ToString());
                }

                if (userId != -1)
                {
                    query = query.Where(l => l.User.Id == userId);
                }

                switch (sortOrder)
                {
                    case "timestamp.asc":
                        query = query.OrderBy(x => x.TimeStamp);
                        break;
                    case "priority.asc":
                        query = query.OrderBy(x => x.PriorityName);
                        break;
                    case "priority.desc":
                        query = query.OrderByDescending(x => x.PriorityName);
                        break;
                    case "message.asc":
                        query = query.OrderBy(x => x.Message);
                        break;
                    case "message.desc":
                        query = query.OrderByDescending(x => x.Message);
                        break;
                    case "destination.asc":
                        query = query.OrderBy(x => x.Url);
                        break;
                    case "destination.desc":
                        query = query.OrderByDescending(x => x.Url);
                        break;
                    case "timestamp.desc":
                    default:
                        query = query.OrderByDescending(x => x.TimeStamp);
                        break;
                }

                return query.ToList();
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
