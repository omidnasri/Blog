using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blog.Repositories
{
    internal interface IRepository<T>
    {
        bool Update(T entity);
        bool Insert(T entity);
        bool Delete(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetBy(Expression<Func<T, bool>> filter);
    }
}
