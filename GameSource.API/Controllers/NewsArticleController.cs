using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers.GameSource
{
    [Route("api/news-articles")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class NewsArticleController : ControllerBase
    {
        private readonly INewsArticleService newsArticleService;

        public NewsArticleController(INewsArticleService newsArticleService)
        {
            this.newsArticleService = newsArticleService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<NewsArticle> result = await newsArticleService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned News Article list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return News Article list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await newsArticleService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a News Article.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a News Article.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] NewsArticle newsArticle)
        {
            int rows = await newsArticleService.InsertAsync(newsArticle);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new News Article.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a News Article.");
        }

        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] NewsArticle newsArticle)
        {
            int rows = await newsArticleService.UpdateAsync(newsArticle);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated News Article.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update News Article.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await newsArticleService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted News Article.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete News Article.");
        }
    }
}