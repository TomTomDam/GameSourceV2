using GameSource.Models.GameSource;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource.Contracts
{
    public interface INewsArticleCategoryRepository : IBaseRepository<NewsArticleCategory>
    {
        public NewsArticleCategory GetByID(int? id);
        public Task<NewsArticleCategory> GetByIDAsync(int? id);
        public bool Delete(int? id);
        public Task<bool> DeleteAsync(int? id);
    }
}
