using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers.GameSource
{
    [Route("api/reviews")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        /// <summary>
        /// Gets all Reviews
        /// </summary>
        /// <response code="200">Returns a list of Reviews</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Review> result = await reviewRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Review list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Review list.");
        }

        /// <summary>
        /// Gets a Review by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a Review</response>
        /// <response code="404">Could not find a Review</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await reviewRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Review.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Review.");
        }

        /// <summary>
        /// Creates a new Review
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new Review</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Review review)
        {
            int rows = await reviewRepository.InsertAsync(review);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Review.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Review.");
        }

        /// <summary>
        /// Updates a Review
        /// </summary>
        /// <param name="id"></param>
        /// <param name="review"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a Review</response>
        /// <response code="404">Could not find a Review</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Review review)
        {
            int rows = await reviewRepository.UpdateAsync(review);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Review.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Review.");
        }

        /// <summary>
        /// Deletes a Review
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a Review</response>
        /// <response code="404">Could not find a Review</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            Review review = await reviewRepository.GetByIDAsync(id);
            if (id == 0 || review == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Review was not found. Please check the ID.");

            int rows = await reviewRepository.DeleteAsync(review);
            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Review.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Review.");
        }
    }
}