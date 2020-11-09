using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GameSource.Data.Repositories.GameSource
{
    public class NewsArticleCategoryRepository : BaseRepository<NewsArticleCategory>, INewsArticleCategoryRepository
    {
        private GameSource_DBContext context;
        private DbSet<NewsArticleCategory> entity;

        public NewsArticleCategoryRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<NewsArticleCategory>();
        }

        public IEnumerable<NewsArticleCategory> GetAll()
        {
            return entity.ToList();
        }

        public NewsArticleCategory GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(NewsArticleCategory newsArticleCategory)
        {
            entity.Add(newsArticleCategory);
            context.SaveChanges();
        }

        public void Update(NewsArticleCategory newsArticleCategory)
        {
            entity.Update(newsArticleCategory);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var newsArticleCategory = GetByID(id);
            entity.Remove(newsArticleCategory);
            context.SaveChanges();
        }
    }
}
