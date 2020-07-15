using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;

namespace GameSource.Services.GameSource
{
    public class GameService : IGameService
    {
        private IGameRepository repo;

        public GameService(IGameRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<Game> GetAll()
        {
            return repo.GetAll();
        }

        public Game GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(Game game)
        {
            repo.Insert(game);
        }

        public void Update(Game game)
        {
            repo.Update(game);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
