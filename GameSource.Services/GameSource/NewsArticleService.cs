using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSource
{
    public class NewsArticleService : BaseService<NewsArticle>, INewsArticleService
    {
        private DbSet<NewsArticle> repo => context.Set<NewsArticle>();

        public NewsArticleService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
