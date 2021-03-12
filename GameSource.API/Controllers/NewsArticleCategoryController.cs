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
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return News Article Category list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned News Article Category list.");
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
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a News Article Category.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a News Article Category.");
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
            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a News Article Category.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new News Article Category.");
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
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update News Article Category.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated News Article Category.");
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
            int rows = await newsArticleCategoryRepository.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete News Article Category.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted News Article Category.");
        }
    }
}