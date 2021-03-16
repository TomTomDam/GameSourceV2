using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GameSource.API.Controllers
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

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned NewsArticleCategory list.", result.Count());
        }

        /// <summary>
        /// Gets a NewsArticleCategory by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a NewsArticleCategory</response>
        /// <response code="404">Could not find a NewsArticleCategory</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int? id)
        {
            if (id == null || id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

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
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a NewsArticleCategory.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new NewsArticleCategory.", rows);
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
        public async Task<ApiResponse> Update(int? id, [FromBody] NewsArticleCategory newsArticleCategory)
        {
            if (id == null || id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            NewsArticleCategory updatedNewsArticleCategory = await newsArticleCategoryRepository.GetByIDAsync(id);

            updatedNewsArticleCategory.Name = newsArticleCategory.Name;

            int rows = await newsArticleCategoryRepository.UpdateAsync(updatedNewsArticleCategory);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update NewsArticleCategory.", rows);

            return new ApiResponse(updatedNewsArticleCategory, ResponseStatusCode.Success, "Successfully updated NewsArticleCategory.", rows);
        }

        /// <summary>
        /// Deletes a NewsArticleCategory
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a NewsArticleCategory</response>
        /// <response code="404">Could not find a NewsArticleCategory</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int? id)
        {
            if (id == null || id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            NewsArticleCategory newsArticleCategory = await newsArticleCategoryRepository.GetByIDAsync(id);
            if (newsArticleCategory == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "NewsArticleCategory was not found. Please check the ID.");

            int rows = await newsArticleCategoryRepository.DeleteAsync(id);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete NewsArticleCategory.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted NewsArticleCategory.", rows);
        }
    }
}