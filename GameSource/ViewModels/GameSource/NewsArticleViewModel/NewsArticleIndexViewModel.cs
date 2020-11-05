using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.NewsArticleViewModel
{
    public class NewsArticleIndexViewModel
    {
        public IEnumerable<NewsArticle> NewsArticles { get; set; }
        public IEnumerable<NewsArticleCategory> NewsArticleCategories { get; set; }
    }
}
