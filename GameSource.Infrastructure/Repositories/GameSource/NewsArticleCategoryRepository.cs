using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class NewsArticleCategoryRepository : BaseRepository<NewsArticleCategory>, INewsArticleCategoryRepository
    {
        private DbSet<NewsArticleCategory> repo => context.Set<NewsArticleCategory>();

        public NewsArticleCategoryRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public NewsArticleCategory GetByID(int? id)
        {
            return repo.Find(id);
        }

        public async Task<NewsArticleCategory> GetByIDAsync(int? id)
        {
            return await repo.FindAsync(id);
        }

        public bool Delete(int? id)
        {
            NewsArticleCategory category = repo.Find(id);
            repo.Remove(category);
            var deleted = context.SaveChanges();
            return deleted > 0;
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            NewsArticleCategory category = await repo.FindAsync(id);
            repo.Remove(category);
            var deleted = await context.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
