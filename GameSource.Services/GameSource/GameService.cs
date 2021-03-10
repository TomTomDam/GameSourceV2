using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSource
{
    public class GameService : BaseService<Game>, IGameService
    {
        private DbSet<Game> repo => context.Set<Game>();

        public GameService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
