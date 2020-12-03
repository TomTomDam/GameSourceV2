using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface INewsArticleCategoryRepository
    {
        public IEnumerable<NewsArticleCategory> GetAll();
        public NewsArticleCategory GetByID(int? id);
        public void Insert(NewsArticleCategory newsArticleCategory);
        public void Update(NewsArticleCategory newsArticleCategory);
        public void Delete(int? id);
        public Task<IEnumerable<NewsArticleCategory>> GetAllAsync();
        public Task<NewsArticleCategory> GetByIDAsync(int? id);
        public Task InsertAsync(NewsArticleCategory newsArticleCategory);
        public Task UpdateAsync(NewsArticleCategory newsArticleCategory);
        public Task DeleteAsync(int? id);
    }
}
