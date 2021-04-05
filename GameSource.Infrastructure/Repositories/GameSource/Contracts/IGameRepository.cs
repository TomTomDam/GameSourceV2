using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource.Contracts
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        public Task<IEnumerable<Platform>> GetPlatformsAsync(Game game);
        public Task<IEnumerable<Review>> GetReviewsAsync(Game game);
    }
}
