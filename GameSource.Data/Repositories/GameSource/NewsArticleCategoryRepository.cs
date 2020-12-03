using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource
{
    public class NewsArticleCategoryRepository : BaseRepository<NewsArticleCategory>, INewsArticleCategoryRepository
    {
        private GameSource_DBContext context;
        private DbSet<NewsArticleCategory> entity;

        public NewsArticleCategoryRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<NewsArticleCategory>();
        }

        public IEnumerable<NewsArticleCategory> GetAll()
        {
            return entity.ToList();
        }

        public NewsArticleCategory GetByID(int? id)
        {
            return entity.Find(id);
        }

        public void Insert(NewsArticleCategory newsArticleCategory)
        {
            entity.Add(newsArticleCategory);
            context.SaveChanges();
        }

        public void Update(NewsArticleCategory newsArticleCategory)
        {
            entity.Update(newsArticleCategory);
            context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var newsArticleCategory = GetByID(id);
            entity.Remove(newsArticleCategory);
            context.SaveChanges();
        }

        public async Task<IEnumerable<NewsArticleCategory>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<NewsArticleCategory> GetByIDAsync(int? id)
        {
            return await entity.FindAsync(id);
        }

        public async Task<NewsArticleCategory> InsertAsync(NewsArticleCategory newsArticleCategory)
        {
            await entity.AddAsync(newsArticleCategory);
            await context.SaveChangesAsync();
            return newsArticleCategory;
        }

        public async Task UpdateAsync(NewsArticleCategory newsArticleCategory)
        {
            entity.Update(newsArticleCategory);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            NewsArticleCategory newsArticleCategory = await GetByIDAsync(id);
            entity.Remove(newsArticleCategory);
            await context.SaveChangesAsync();
        }
    }
}
