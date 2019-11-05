using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager.Logic.Common
{
    public interface IRepository<TEntity, TId>
    { 
        IEnumerable<TEntity> List();
        TEntity GetById(TId id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TId id);
    }
}
