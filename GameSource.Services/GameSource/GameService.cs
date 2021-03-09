using GameSource.Infrastructure;
using GameSource.Models;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSource
{
    public class GameService : BaseService<Game>, IGameService
    {
        private GameSource_DBContext context;
        private DbSet<Game> repo => context.Set<Game>();

        public GameService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
