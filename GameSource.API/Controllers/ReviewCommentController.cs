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
    [Route("api/review-comments")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class ReviewCommentController : ControllerBase
    {
        private readonly IReviewCommentRepository reviewCommentRepository;

        public ReviewCommentController(IReviewCommentRepository reviewCommentRepository)
        {
            this.reviewCommentRepository = reviewCommentRepository;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<ReviewComment> result = await reviewCommentRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Review list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Review list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await reviewCommentRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Review Comment.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Review Comment.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] ReviewComment reviewComment)
        {
            int rows = await reviewCommentRepository.InsertAsync(reviewComment);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Review Comment.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Review Comment.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] ReviewComment reviewComment)
        {
            int rows = await reviewCommentRepository.UpdateAsync(reviewComment);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Review Comment.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Review Comment.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await reviewCommentRepository.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Review Comment.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Review Comment.");
        }
    }
}