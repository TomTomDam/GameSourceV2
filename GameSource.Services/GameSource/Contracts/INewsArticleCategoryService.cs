using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource.Contracts
{
    public interface INewsArticleCategoryService : IBaseService<NewsArticleCategory>
    {
        public NewsArticleCategory GetByID(int? id);
        public void Delete(int? id);
        public Task<NewsArticleCategory> GetByIDAsync(int? id);
        public Task DeleteAsync(int? id);
    }
}
