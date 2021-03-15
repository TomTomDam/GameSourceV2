using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        private DbSet<Game> repo => context.Set<Game>();

        public GameRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
