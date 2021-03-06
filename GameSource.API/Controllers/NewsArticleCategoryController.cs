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
                return new ApiResponse(ResponseStatusCode.NotFound, "Could not return a NewsArticleCategory.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a NewsArticleCategory.", 1);
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
        public async Task<ApiResponse> Insert([FromBody] NewsArticleCategory category)
        {
            var inserted = await newsArticleCategoryRepository.InsertAsync(category);
            if (!inserted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a NewsArticleCategory.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new NewsArticleCategory.", 1);
        }

        /// <summary>
        /// Updates a NewsArticleCategory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
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
        public async Task<ApiResponse> Update(int? id, [FromBody] NewsArticleCategory category)
        {
            if (id == null || id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            NewsArticleCategory updatedCategory = await newsArticleCategoryRepository.GetByIDAsync(id);
            if (updatedCategory == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "NewsArticleCategory was not found.");

            updatedCategory.Name = category.Name;

            var updated = await newsArticleCategoryRepository.UpdateAsync(updatedCategory);
            if (!updated)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update NewsArticleCategory.", 0);

            return new ApiResponse(updatedCategory, ResponseStatusCode.Success, "Successfully updated NewsArticleCategory.", 1);
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

            NewsArticleCategory category = await newsArticleCategoryRepository.GetByIDAsync(id);
            if (category == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "NewsArticleCategory was not found.");

            var deleted = await newsArticleCategoryRepository.DeleteAsync(id);
            if (!deleted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete NewsArticleCategory.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted NewsArticleCategory.", 1);
        }
    }
}