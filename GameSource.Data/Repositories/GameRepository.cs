using GameSource.Data.Repositories.Contracts;
using GameSource.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Data.Repositories
{
    public class GameRepository : IBaseRepository<Game>, IGameRepository
    {
        protected readonly GameSource_DBContext context;
        private DbSet<Game> gameEntity;

        public GameRepository(GameSource_DBContext context)
        {
            this.context = context;
            gameEntity = context.Set<Game>();
        }

        public IEnumerable<Game> GetAll()
        {
            return gameEntity.ToList();
        }

        public Game GetByID(int id)
        {
            return gameEntity.Find(id);
        }

        public void Insert(Game game)
        {
            gameEntity.Add(game);
        }

        public void Update(Game game)
        {
            gameEntity.Update(game);
        }

        public void Delete(int id)
        {
            var game = GetByID(id);
            gameEntity.Remove(game);
        }
    }
}
