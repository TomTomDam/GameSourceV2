using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;

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
    }
}
