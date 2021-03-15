using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.API.Controllers.GameSource
{
    [Route("api/news-article-categories")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class NewsArticleCategoryController : ControllerBase
    {
        private readonly INewsArticleCategoryRepository newsArticleCategoryRepository;

        public NewsArticleCategoryController(INewsArticleCategoryRepository newsArticleCategoryRepository)
        {
            this.newsArticleCategoryRepository = newsArticleCategoryRepository;
        }

        /// <summary>
        /// Gets all NewsArticleCategories
        /// </summary>
        /// <response code="200">Returns a list of NewsArticleCategory</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<NewsArticleCategory> result = await newsArticleCategoryRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return NewsArticleCategory list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned NewsArticleCategory list.");
        }

        /// <summary>
        /// Gets a NewsArticleCategory by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a NewsArticleCategory</response>
        /// <response code="404">Could not find a NewsArticleCategory</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await newsArticleCategoryRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a NewsArticleCategory.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a NewsArticleCategory.");
        }

        /// <summary>
        /// Creates a new NewsArticleCategory
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Promotion"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new NewsArticleCategory</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] NewsArticleCategory newsArticleCategory)
        {
            int rows = await newsArticleCategoryRepository.InsertAsync(newsArticleCategory);

            if (rows <= 0)
            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a NewsArticleCategory.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new NewsArticleCategory.");
        }

        /// <summary>
        /// Updates a NewsArticleCategory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newsArticleCategory"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Promotion"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a NewsArticleCategory</response>
        /// <response code="404">Could not find a NewsArticleCategory</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] NewsArticleCategory newsArticleCategory)
        {
            int rows = await newsArticleCategoryRepository.UpdateAsync(newsArticleCategory);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update NewsArticleCategory.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated NewsArticleCategory.");
        }

        /// <summary>
        /// Deletes a NewsArticleCategory
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a NewsArticleCategory</response>
        /// <response code="404">Could not find a NewsArticleCategory</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            NewsArticleCategory newsArticleCategory = await newsArticleCategoryRepository.GetByIDAsync(id);
            if (id == 0 || newsArticleCategory == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "NewsArticleCategory was not found. Please check the ID.");

            int rows = await newsArticleCategoryRepository.DeleteAsync(id);
            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete NewsArticleCategory.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted NewsArticleCategory.");
        }
    }
}