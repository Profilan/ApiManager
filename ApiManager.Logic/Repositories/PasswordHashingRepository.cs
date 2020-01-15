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
    public class PasswordHashingRepository : IRepository<PasswordHashing, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PasswordHashing GetById(int id)
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var item = session.Get<PasswordHashing>(id);

                return item;
            }
        }

        public void Insert(PasswordHashing entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PasswordHashing> List()
        {
            using (ISession session = SessionFactory.GetNewSession("db1"))
            {
                var query = session.Query<PasswordHashing>().OrderBy(x => x.Code);

                return query.ToList();
            }
        }

        public void Update(PasswordHashing entity)
        {
            throw new NotImplementedException();
        }
    }
}
