using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface IGameRepository
    {
        public IEnumerable<Game> GetAll();
        public Game GetByID(int id);
        public void Insert(Game game);
        public void Update(Game game);
        public void Delete(int id);
        public Task<IEnumerable<Game>> GetAllAsync();
        public Task<Game> GetByIDAsync(int id);
        public Task InsertAsync(Game game);
        public Task UpdateAsync(Game game);
        public Task DeleteAsync(int id);
    }
}
