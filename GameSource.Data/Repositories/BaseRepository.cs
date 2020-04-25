using GameSource.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameSource.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly GameSource_DBContext context;
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
        }

        public void Update(T item)
        {
            entity.Update(item);
        }

        public void Delete(int id)
        {
            var item = GetByID(id);
            entity.Remove(item);
        }
    }
}
