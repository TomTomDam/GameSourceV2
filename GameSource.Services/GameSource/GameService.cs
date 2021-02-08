using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<Game> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task InsertAsync(Game game)
        {
            await repo.InsertAsync(game);
        }

        public async Task UpdateAsync(Game game)
        {
            await repo.UpdateAsync(game);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
