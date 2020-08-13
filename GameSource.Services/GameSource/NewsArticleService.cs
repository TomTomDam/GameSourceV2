using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;

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
    }
}
