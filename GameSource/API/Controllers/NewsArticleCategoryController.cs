using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.API.Controllers
{
    [Route("api/news-article-category")]
    [ApiController]
    public class NewsArticleCategoryController : ControllerBase
    {
        private readonly INewsArticleCategoryService newsArticleCategoryService;

        public NewsArticleCategoryController(INewsArticleCategoryService newsArticleCategoryService)
        {
            this.newsArticleCategoryService = newsArticleCategoryService;
        }

        [HttpGet("GetAll")]
        public IEnumerable<NewsArticleCategory> GetAll()
        {
            return newsArticleCategoryService.GetAll();
        }

        [HttpGet("GetAllAsync")]
        public async Task<ApiResponse> GetAllAsync()
        {
            var result = await newsArticleCategoryService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Users list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Users list.");
        }
    }
}