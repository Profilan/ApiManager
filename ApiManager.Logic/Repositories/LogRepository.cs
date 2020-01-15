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
    public class LogRepository : IRepository<Log, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Log GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Log entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Log> List()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Log> List(string sortOrder, string searchString, int pageNumber, int pageSize, DateTime startDate, DateTime endDate, int userId = -1, ErrorType errorType = ErrorType.NONE)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<Log>()
                            select l;

                // query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(x => x.Url.Contains(searchString)
                        || x.Message.Contains(searchString)
                        || x.Detail.Contains(searchString)
                        || x.PriorityName.Contains(searchString));
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

        public void Update(Log entity)
        {
            throw new NotImplementedException();
        }
    }
}
