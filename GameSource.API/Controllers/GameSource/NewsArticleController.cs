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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class NewsArticleController : ControllerBase
    {
        private readonly INewsArticleService newsArticleService;

        public NewsArticleController(INewsArticleService newsArticleService)
        {
            this.newsArticleService = newsArticleService;
        }

        [HttpGet("GetAll")]
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
        public async Task Insert([FromBody] NewsArticle newsArticle)
        {
            await newsArticleService.InsertAsync(newsArticle);
        }

        [HttpPost]
        public async Task Update([FromBody] NewsArticle newsArticle)
        {
            await newsArticleService.UpdateAsync(newsArticle);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await newsArticleService.DeleteAsync(id);
        }
    }
}