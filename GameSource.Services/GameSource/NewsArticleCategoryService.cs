using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class NewsArticleCategoryService : BaseService<NewsArticleCategory>, INewsArticleCategoryService
    {
        private readonly GameSource_DBContext context;
        private DbSet<NewsArticleCategory> repo => context.Set<NewsArticleCategory>();

        public NewsArticleCategoryService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public NewsArticleCategory GetByID(int? id)
        {
            return repo.Find(id);
        }

        public void Delete(int? id)
        {
            NewsArticleCategory category = repo.Find(id);
            repo.Remove(category);
            context.SaveChanges();
        }

        public async Task<NewsArticleCategory> GetByIDAsync(int? id)
        {
            return await repo.FindAsync(id);
        }

        public async Task DeleteAsync(int? id)
        {
            NewsArticleCategory category = await repo.FindAsync(id);
            repo.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
