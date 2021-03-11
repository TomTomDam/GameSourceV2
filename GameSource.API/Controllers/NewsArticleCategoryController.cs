using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.API.Controllers.GameSource
{
    [Route("api/news-article-categories")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class NewsArticleCategoryController : ControllerBase
    {
        private readonly INewsArticleCategoryService newsArticleCategoryService;

        public NewsArticleCategoryController(INewsArticleCategoryService newsArticleCategoryService)
        {
            this.newsArticleCategoryService = newsArticleCategoryService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<NewsArticleCategory> result = await newsArticleCategoryService.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return News Article Category list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned News Article Category list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await newsArticleCategoryService.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a News Article Category.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a News Article Category.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] NewsArticleCategory newsArticleCategory)
        {
            int rows = await newsArticleCategoryService.InsertAsync(newsArticleCategory);

            if (rows <= 0)
            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a News Article Category.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new News Article Category.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] NewsArticleCategory newsArticleCategory)
        {
            int rows = await newsArticleCategoryService.UpdateAsync(newsArticleCategory);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update News Article Category.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated News Article Category.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await newsArticleCategoryService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete News Article Category.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted News Article Category.");
        }
    }
}