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

        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<NewsArticleCategory> result = await newsArticleCategoryService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned NewsArticleCategory list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return NewsArticleCategory list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await newsArticleCategoryService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User.");
        }

        [HttpPost]
        public async Task Insert([FromBody] NewsArticleCategory category)
        {
            await newsArticleCategoryService.InsertAsync(category);
        }

        [HttpPost]
        public async Task Update([FromBody] NewsArticleCategory category)
        {
            await newsArticleCategoryService.UpdateAsync(category);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await newsArticleCategoryService.DeleteAsync(id);
        }
    }
}