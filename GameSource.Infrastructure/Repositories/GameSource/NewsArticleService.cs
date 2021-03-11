using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class NewsArticleRepository : BaseRepository<NewsArticle>, INewsArticleRepository
    {
        private DbSet<NewsArticle> repo => context.Set<NewsArticle>();

        public NewsArticleRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
