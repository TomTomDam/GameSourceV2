using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GameSource.Data.Repositories.GameSource
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        private GameSource_DBContext context;
        private DbSet<Genre> entity;

        public GenreRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<Genre>();
        }

        public IEnumerable<Genre> GetAll() 
        {
            return entity.ToList();
        }

        public Genre GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(Genre genre)
        {
            entity.Add(genre);
            context.SaveChanges();
        }

        public void Update(Genre genre)
        {
            entity.Update(genre);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var genre = GetByID(id);
            entity.Remove(genre);
            context.SaveChanges();
        }
    }
}
