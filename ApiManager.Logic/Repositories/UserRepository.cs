using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiManager.Logic.Repositories
{
    public class UserRepository : IRepository<User, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Get<User>(id);

                return item;
            }
        }

        public void Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<User>()
                    
                    .OrderBy(x => x.Username);

                return query.ToList();
            }
        }

        public IEnumerable<User> List(string sortOrder, string searchString)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = from l in session.Query<User>()
                            select l;

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(x => x.DisplayName.Contains(searchString)
                                           || x.Email.Contains(searchString)
                                           || x.Username.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "created.asc":
                        query = query.OrderBy(x => x.SysCreated);
                        break;
                    case "created.desc":
                        query = query.OrderByDescending(x => x.SysCreated);
                        break;
                    case "email.asc":
                        query = query.OrderBy(x => x.Email);
                        break;
                    case "email.desc":
                        query = query.OrderByDescending(x => x.Email);
                        break;
                    case "displayname.desc":
                        query = query.OrderByDescending(x => x.DisplayName);
                        break;
                    case "displayname.asc":
                    default:
                        query = query.OrderBy(x => x.DisplayName);
                        break;
                }

                return query.ToList();
            }
        }

        public void Update(User entity)
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
