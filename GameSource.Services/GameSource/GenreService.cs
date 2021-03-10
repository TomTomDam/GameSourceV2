using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSource
{
    public class GenreService : BaseService<Genre>, IGenreService
    {
        private readonly GameSource_DBContext context;
        private DbSet<Genre> repo => context.Set<Genre>();

        public GenreService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
