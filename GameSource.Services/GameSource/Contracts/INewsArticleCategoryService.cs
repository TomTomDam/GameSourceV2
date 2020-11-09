using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.Services.GameSource.Contracts
{
    public interface INewsArticleCategoryService
    {
        public IEnumerable<NewsArticleCategory> GetAll();
        public NewsArticleCategory GetByID(int id);
        public void Insert(NewsArticleCategory newsArticleCategory);
        public void Update(NewsArticleCategory newsArticleCategory);
        public void Delete(int id);
    }
}
