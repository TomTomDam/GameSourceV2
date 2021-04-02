using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameSource.Models.DTOs.GameSource;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        private DbSet<Game> repo => context.Set<Game>();

        public GameRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Platform>> GetPlatformAsync(Game game)
        {
            await context.Entry(game).Collection(g => g.Platforms).LoadAsync();
            return game.Platforms;
        }

        public async Task<IEnumerable<Review>> GetReviewAsync(Game game)
        {
            await context.Entry(game).Collection(g => g.Reviews).LoadAsync();
            return game.Reviews;
        }
    }
}
