using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        private DbSet<Genre> repo => context.Set<Genre>();

        public GenreRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
