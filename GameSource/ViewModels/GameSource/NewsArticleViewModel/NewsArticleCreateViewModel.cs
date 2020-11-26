using GameSource.Models.GameSource;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.NewsArticleViewModel
{
    public class NewsArticleCreateViewModel
    {
        public NewsArticle NewsArticle { get; set; }
        public List<SelectListItem> NewsArticleCategory { get; set; }
    }
}
