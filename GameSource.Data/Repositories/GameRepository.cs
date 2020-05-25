using GameSource.Data.Repositories.Contracts;
using GameSource.Models;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Data.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        private GameSource_DBContext context;
        private DbSet<Game> entity;

        public GameRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<Game>();
        }

        public IEnumerable<Game> GetAll()
        {
            return entity.ToList();
        }

        public Game GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(Game game)
        {
            entity.Add(game);
        }

        public void Update(Game game)
        {
            entity.Update(game);
        }

        public void Delete(int id)
        {
            var game = GetByID(id);
            entity.Remove(game);
        }
    }
}
