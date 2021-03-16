using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace GameSource.API.Controllers.GameSource
{
    [Route("api/news-articles")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class NewsArticleController : ControllerBase
    {
        private readonly INewsArticleRepository newsArticleRepository;

        public NewsArticleController(INewsArticleRepository newsArticleRepository)
        {
            this.newsArticleRepository = newsArticleRepository;
        }

        /// <summary>
        /// Gets all NewsArticles
        /// </summary>
        /// <response code="200">Returns a list of NewsArticle</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<NewsArticle> result = await newsArticleRepository.GetAllAsync();

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned News Article list.", result.Count());
        }

        /// <summary>
        /// Gets a NewsArticle by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a NewsArticle</response>
        /// <response code="404">Could not find a NewsArticle</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var result = await newsArticleRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.NotFound, "Could not find a News Article.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a News Article.");
        }

        /// <summary>
        /// Creates a new NewsArticle
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "title": "Welcome to GameSource!"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new NewsArticle</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] NewsArticle newsArticle)
        {
            int rows = await newsArticleRepository.InsertAsync(newsArticle);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a News Article.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new News Article.", rows);
        }

        /// <summary>
        /// Updates a NewsArticle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newsArticle"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "title": "Welcome to GameSource!"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a NewsArticle</response>
        /// <response code="404">Could not find a NewsArticle</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] NewsArticle newsArticle)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            NewsArticle updatedNewsArticle = await newsArticleRepository.GetByIDAsync(id);
            if (updatedNewsArticle == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Could not find a News Article.");

            updatedNewsArticle.Title = newsArticle.Title;
            updatedNewsArticle.Body = newsArticle.Body;
            updatedNewsArticle.CreatedByID = newsArticle.CreatedByID;
            updatedNewsArticle.DateModified = DateTime.Now;

            int rows = await newsArticleRepository.UpdateAsync(updatedNewsArticle);
            if (rows <= 0)
                return new ApiResponse(updatedNewsArticle, ResponseStatusCode.Error, "Could not update News Article.", rows);

            return new ApiResponse(updatedNewsArticle, ResponseStatusCode.Success, "Successfully updated News Article.", rows);
        }

        /// <summary>
        /// Deletes a NewsArticle
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a NewsArticle</response>
        /// <response code="404">Could not find a NewsArticle</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            NewsArticle newsArticle = await newsArticleRepository.GetByIDAsync(id);
            if (newsArticle == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "NewsArticle was not found. Please check the ID.");

            int rows = await newsArticleRepository.DeleteAsync(newsArticle);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete News Article.");

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted News Article.", rows);
        }
    }
}