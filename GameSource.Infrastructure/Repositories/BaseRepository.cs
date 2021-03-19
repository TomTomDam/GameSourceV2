using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected GameSource_DBContext context;
        protected DbSet<T> entity;

        public BaseRepository(GameSource_DBContext context)
        {
            this.context = context;
            entity = context.Set<T>();
        }

        public bool Delete(T item)
        {
            entity.Remove(item);
            var deleted = context.SaveChanges();
            return deleted > 0;
        }

        public async Task<bool> DeleteAsync(T item)
        {
            entity.Remove(item);
            var deleted = await context.SaveChangesAsync();
            return deleted > 0;
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

        public bool Insert(T item)
        {
            entity.Add(item);
            var inserted = context.SaveChanges();
            return inserted > 0;
        }

        public async Task<bool> InsertAsync(T item)
        {
            await entity.AddAsync(item);
            var inserted = await context.SaveChangesAsync();
            return inserted > 0;
        }

        public bool Update(T item)
        {
            entity.Update(item);
            var updated = context.SaveChanges();
            return updated > 0;
        }

        public async Task<bool> UpdateAsync(T item)
        {
            entity.Update(item);
            var updated = await context.SaveChangesAsync();
            return updated > 0;
        }
    }
}
