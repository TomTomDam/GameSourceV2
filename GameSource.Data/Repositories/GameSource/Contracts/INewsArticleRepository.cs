using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface INewsArticleRepository
    {
        public IEnumerable<NewsArticle> GetAll();
        public NewsArticle GetByID(int id);
        public void Insert(NewsArticle newsArticle);
        public void Update(NewsArticle newsArticle);
        public void Delete(int id);
        public Task<IEnumerable<NewsArticle>> GetAllAsync();
        public Task<NewsArticle> GetByIDAsync(int id);
        public Task InsertAsync(NewsArticle newsArticle);
        public Task UpdateAsync(NewsArticle newsArticle);
        public Task DeleteAsync(int id);
    }
}
