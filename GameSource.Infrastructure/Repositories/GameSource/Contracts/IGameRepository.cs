using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource.Contracts
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        public new Task<IEnumerable<Game>> GetAllAsync();
        public new Task<Game> GetByIDAsync(int id);
    }
}
