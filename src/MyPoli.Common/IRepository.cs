using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoli.Common
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> Get();
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entitty);
        void Delete(TEntity entity);
    }
}
