﻿using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.Services.GameSource.Contracts
{
    public interface INewsArticleService
    {
        public IEnumerable<NewsArticle> GetAll();
        public NewsArticle GetByID(int id);
        public void Insert(NewsArticle newsArticle);
        public void Update(NewsArticle newsArticle);
        public void Delete(int id);
    }
}