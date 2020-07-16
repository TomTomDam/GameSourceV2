using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IBaseService<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetByID(int id);
        public void Insert(T item);
        public void Update(T item);
        public void Delete(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIDAsync(int id);
        public Task InsertAsync(T item);
        public Task UpdateAsync(T item);
        public Task DeleteAsync(int id);
    }
}
