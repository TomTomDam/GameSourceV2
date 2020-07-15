using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Data.Repositories.GameSource
{
    public class PlatformTypeRepository : BaseRepository<PlatformType>, IPlatformTypeRepository
    {
        private GameSource_DBContext context;
        private DbSet<PlatformType> entity;

        public PlatformTypeRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<PlatformType>();
        }

        public IEnumerable<PlatformType> GetAll()
        {
            return entity.ToList();
        }

        public PlatformType GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(PlatformType platformType)
        {
            entity.Add(platformType);
            context.SaveChanges();
        }

        public void Update(PlatformType platformType)
        {
            entity.Update(platformType);
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
