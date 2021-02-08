using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class NewsArticleService : INewsArticleService
    {
        private INewsArticleRepository repo;

        public NewsArticleService(INewsArticleRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<NewsArticle> GetAll()
        {
            return repo.GetAll();
        }

        public NewsArticle GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(NewsArticle newsArticle)
        {
            repo.Insert(newsArticle);
        }

        public void Update(NewsArticle newsArticle)
        {
            repo.Update(newsArticle);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<NewsArticle>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<NewsArticle> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task InsertAsync(NewsArticle newsArticle)
        {
            await repo.InsertAsync(newsArticle);
        }

        public async Task UpdateAsync(NewsArticle newsArticle)
        {
            await repo.UpdateAsync(newsArticle);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
