using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.API.Controllers
{
    [Route("api/news-article-category")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class NewsArticleCategoryController : ControllerBase
    {
        private readonly INewsArticleCategoryService newsArticleCategoryService;

        public NewsArticleCategoryController(INewsArticleCategoryService newsArticleCategoryService)
        {
            this.newsArticleCategoryService = newsArticleCategoryService;
        }

        [HttpGet("GetAllAsync")]
        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<NewsArticleCategory> result = await newsArticleCategoryService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned NewsArticleCategory list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return NewsArticleCategory list.");
        }
    }
}