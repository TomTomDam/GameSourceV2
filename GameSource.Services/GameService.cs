using GameSource.Data.Repositories;
using GameSource.Data.Repositories.Contracts;
using GameSource.Models;
using GameSource.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services
{
    public class GameService : IGameService
    {
        private IGameRepository gameRepo;

        public GameService(IGameRepository gameRepo)
        {
            this.gameRepo = gameRepo;
        }

        public IEnumerable<Game> GetAll()
        {
            return gameRepo.GetAll();
        }

        public Game GetByID(int id)
        {
            return gameRepo.GetByID(id);
        }

        public void Insert(Game game)
        {
            gameRepo.Insert(game);
        }

        public void Update(Game game)
        {
            gameRepo.Update(game);
        }

        public void Delete(int id)
        {
            gameRepo.Delete(id);
        }
    }
}
