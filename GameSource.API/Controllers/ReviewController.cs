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
    [Route("api/reviews")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Review> result = await reviewService.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Review list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Review list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await reviewService.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Review.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Review.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Review review)
        {
            int rows = await reviewService.InsertAsync(review);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Review.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Review.");
        }

        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] Review review)
        {
            int rows = await reviewService.UpdateAsync(review);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Review.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Review.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await reviewService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Review.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Review.");
        }
    }
}