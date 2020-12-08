using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class NewsArticleCategoryService : INewsArticleCategoryService
    {
        private INewsArticleCategoryRepository repo;

        public NewsArticleCategoryService(INewsArticleCategoryRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<NewsArticleCategory> GetAll()
        {
            return repo.GetAll();
        }

        public NewsArticleCategory GetByID(int? id)
        {
            return repo.GetByID(id);
        }

        public void Insert(NewsArticleCategory newsArticleCategory)
        {
            repo.Insert(newsArticleCategory);
        }

        public void Update(NewsArticleCategory newsArticleCategory)
        {
            repo.Update(newsArticleCategory);
        }

        public void Delete(int? id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<NewsArticleCategory>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<NewsArticleCategory> GetByIDAsync(int? id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task<NewsArticleCategory> InsertAsync(NewsArticleCategory newsArticleCategory)
        {
            return await repo.InsertAsync(newsArticleCategory);
        }

        public async Task UpdateAsync(NewsArticleCategory newsArticleCategory)
        {
            await repo.UpdateAsync(newsArticleCategory);
        }

        public async Task DeleteAsync(int? id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
