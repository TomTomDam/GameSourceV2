using GameSource.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Data.Repositories
{
    public class DeveloperRepository : BaseRepository<Developer>, IDeveloperRepository
    {
        private GameSource_DBContext context;
        private DbSet<Developer> entity;

        public DeveloperRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<Developer>();
        }

        public IEnumerable<Developer> GetAll()
        {
            return entity.ToList();
        }

        public Developer GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(Developer developer)
        {
            entity.Add(developer);
            context.SaveChanges();
        }

        public void Update(Developer developer)
        {
            entity.Update(developer);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var developer = GetByID(id);
            entity.Remove(developer);
            context.SaveChanges();
        }
    }
}
