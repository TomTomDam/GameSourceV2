using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Data.Repositories.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        public T GetByID(int id);
        public IEnumerable<T> GetAll();
        public void Insert(T item);
        public void Update(T item);
        public void Delete(int id);
    }
}
