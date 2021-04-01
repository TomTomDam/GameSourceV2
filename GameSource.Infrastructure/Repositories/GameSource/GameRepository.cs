using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        private DbSet<Game> repo => context.Set<Game>();

        public GameRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public new async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await entity.Include(x => x.Platforms).ToListAsync();
        }

        public new async Task<Game> GetByIDAsync(int id)
        {
            var item = await entity.FindAsync(id);
            return item;
        }
    }
}
