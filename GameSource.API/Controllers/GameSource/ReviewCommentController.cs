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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class ReviewCommentController : ControllerBase
    {
        private readonly IReviewCommentService reviewCommentService;

        public ReviewCommentController(IReviewCommentService reviewCommentService)
        {
            this.reviewCommentService = reviewCommentService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<ReviewComment> result = await reviewCommentService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Review list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Review list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await reviewCommentService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User.");
        }

        [HttpPost]
        public async Task Insert([FromBody] ReviewComment review)
        {
            await reviewCommentService.InsertAsync(review);
        }

        [HttpPost]
        public async Task Update([FromBody] ReviewComment review)
        {
            await reviewCommentService.UpdateAsync(review);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await reviewCommentService.DeleteAsync(id);
        }
    }
}