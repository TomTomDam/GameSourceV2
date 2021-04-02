using GameSource.Models.DTOs.GameSource;
using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource.Contracts
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        public Task<IEnumerable<Platform>> GetPlatformAsync(Game game);
        public Task<IEnumerable<Review>> GetReviewAsync(Game game);
    }
}
