using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FriGo.Db.DAL
{
    public interface IGenericRepository<T>
    {
        T GetById(object id);

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        IEnumerable<T> GetAll();

        void Edit(T entity);

        void Insert(T entity);

        void Delete(T entity);

        void Delete(object id);

        void Save();
    }
}