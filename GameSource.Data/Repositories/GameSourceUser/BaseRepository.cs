using GameSource.Data.Repositories.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private GameSourceUser_DBContext context;
        private DbSet<T> entity;

        public BaseRepository(GameSourceUser_DBContext context)
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
            var item = GetByID(id);
            entity.Remove(item);
            context.SaveChanges();
        }
    }
}
