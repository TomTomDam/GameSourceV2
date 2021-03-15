using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource.Contracts
{
    public interface INewsArticleCategoryRepository : IBaseRepository<NewsArticleCategory>
    {
        public NewsArticleCategory GetByID(int? id);
        public int Delete(int? id);
        public Task<NewsArticleCategory> GetByIDAsync(int? id);
        public Task<int> DeleteAsync(int? id);
    }
}
