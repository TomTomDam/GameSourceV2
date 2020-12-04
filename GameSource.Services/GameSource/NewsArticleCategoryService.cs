using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class NewsArticleCategoryService : INewsArticleCategoryService
    {
        INewsArticleCategoryRepository repo;

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
            throw new System.NotImplementedException();
        }

        public async Task<NewsArticleCategory> GetByIDAsync(int? id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<NewsArticleCategory> InsertAsync(NewsArticleCategory newsArticleCategory)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateAsync(NewsArticleCategory newsArticleCategory)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(int? id)
        {
            throw new System.NotImplementedException();
        }
    }
}
