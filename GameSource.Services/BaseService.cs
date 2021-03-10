using GameSource.Infrastructure;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly GameSource_DBContext context;
        protected readonly DbSet<T> entity;

        public BaseService(GameSource_DBContext context)
        {
            this.context = context;
            entity = context.Set<T>();
        }

        public int Delete(int id)
        {
            T item = entity.Find(id);
            entity.Remove(item);
            return context.SaveChanges();
        }

        public async Task<int> DeleteAsync(int id)
        {
            T item = await entity.FindAsync(id);
            entity.Remove(item);
            return await context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return entity.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public T GetByID(int id)
        {
            T item = entity.Find(id);
            return item;
        }

        public async Task<T> GetByIDAsync(int id)
        {
            T item = await entity.FindAsync(id);
            return item;
        }

        public int Insert(T item)
        {
            entity.Add(item);
            return context.SaveChanges();
        }

        public async Task<int> InsertAsync(T item)
        {
            await entity.AddAsync(item);
            return await context.SaveChangesAsync();
        }

        public int Update(T item)
        {
            entity.Update(item);
            return context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T item)
        {
            entity.Update(item);
            return await context.SaveChangesAsync();
        }
    }
}
