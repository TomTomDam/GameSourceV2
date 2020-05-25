using GameSource.Data.Repositories.Contracts;
using GameSource.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Data.Repositories
{
    public class PlatformRepository : BaseRepository<Platform>, IPlatformRepository
    {
        private GameSource_DBContext context;
        private DbSet<Platform> entity; 

        public PlatformRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<Platform>();
        }

        public IEnumerable<Platform> GetAll()
        {
            return entity.ToList();
        }

        public Platform GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(Platform platform)
        {
            entity.Add(platform);
            context.SaveChanges();
        }

        public void Update(Platform platform)
        {
            entity.Update(platform);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var platform = GetByID(id);
            entity.Remove(platform);
            context.SaveChanges();
        }
    }
}
