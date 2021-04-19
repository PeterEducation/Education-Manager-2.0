using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll(string searchString);

        T GetById(Guid id);

        void Create(T entity);

        void Update(T entity);

        void Delete(Guid id);

        void Delete(T entity);
    }
}
