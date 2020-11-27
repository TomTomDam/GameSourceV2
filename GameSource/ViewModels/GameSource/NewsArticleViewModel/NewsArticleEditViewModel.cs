using GameSource.Models.GameSource;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.NewsArticleViewModel
{
    public class NewsArticleEditViewModel
    {
        public NewsArticle NewsArticle { get; set; }
        public int? CategoryID { get; set; }
        public List<SelectListItem> NewsArticleCategories { get; set; }
    }
}
