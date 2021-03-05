using GameSource.Data.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private GameSource_DBContext context;
        private DbSet<T> entity;

        public BaseRepository(GameSource_DBContext context)
        {
            this.context = context;
            entity = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entity.ToList();
        }

        public T GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(T item)
        {
            entity.Add(item);
            context.SaveChanges();
        }

        public void Update(T item)
        {
            entity.Update(item);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            T item = GetByID(id);
            entity.Remove(item);
            context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task InsertAsync(T item)
        {
            await entity.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            entity.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            T item = await GetByIDAsync(id);
            entity.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
