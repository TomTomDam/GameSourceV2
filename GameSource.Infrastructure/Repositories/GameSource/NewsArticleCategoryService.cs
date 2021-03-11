﻿using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class NewsArticleCategoryRepository : BaseRepository<NewsArticleCategory>, INewsArticleCategoryRepository
    {
        private DbSet<NewsArticleCategory> repo => context.Set<NewsArticleCategory>();

        public NewsArticleCategoryRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public NewsArticleCategory GetByID(int? id)
        {
            return repo.Find(id);
        }

        public int Delete(int? id)
        {
            NewsArticleCategory category = repo.Find(id);
            repo.Remove(category);
            return context.SaveChanges();
        }

        public async Task<NewsArticleCategory> GetByIDAsync(int? id)
        {
            return await repo.FindAsync(id);
        }

        public async Task<int> DeleteAsync(int? id)
        {
            NewsArticleCategory category = await repo.FindAsync(id);
            repo.Remove(category);
            return await context.SaveChangesAsync();
        }
    }
}