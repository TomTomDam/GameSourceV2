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
    [EnableCors("AllowOrigin")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Review> result = await reviewRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Review list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Review list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await reviewRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Review.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Review.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Review review)
        {
            int rows = await reviewRepository.InsertAsync(review);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Review.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Review.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Review review)
        {
            int rows = await reviewRepository.UpdateAsync(review);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Review.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Review.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await reviewRepository.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Review.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Review.");
        }
    }
}