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
using GameSource.API.Providers;

namespace GameSource.API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IDateTimeProvider dateTimeProvider;

        public ReviewController(IReviewRepository reviewRepository, IDateTimeProvider dateTimeProvider)
        {
            this.reviewRepository = reviewRepository;
            this.dateTimeProvider = dateTimeProvider;
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

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Review list.", result.Count());
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
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var result = await reviewRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Review was not found.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Review.", 1);
        }

        /// <summary>
        /// Creates a new Review
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "I'm Commander Shepard, and this is my favourite store on the Citadel."
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new Review</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Review review)
        {
            var inserted = await reviewRepository.InsertAsync(review);
            if (!inserted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a Review.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new Review.", 1);
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
        ///         "name": "I'm Commander Shepard, and this is my favourite store on the Citadel."
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a Review</response>
        /// <response code="404">Could not find a Review</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Review review)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var updatedReview = await reviewRepository.GetByIDAsync(id);
            if (updatedReview == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Review was not found.");

            updatedReview.Title = review.Title;
            updatedReview.Body = review.Body;
            updatedReview.DateModified = dateTimeProvider.Now;
            updatedReview.HelpfulRating = review.HelpfulRating;

            var updated = await reviewRepository.UpdateAsync(updatedReview);
            if (!updated)
                return new ApiResponse(updatedReview, ResponseStatusCode.Error, "Could not update Review.", 0);

            return new ApiResponse(updatedReview, ResponseStatusCode.Success, "Successfully updated Review.", 1);
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
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            Review review = await reviewRepository.GetByIDAsync(id);
            if (review == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Review was not found. Please check the ID.");

            var deleted = await reviewRepository.DeleteAsync(review);
            if (!deleted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete Review.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted Review.", 1);
        }
    }
}