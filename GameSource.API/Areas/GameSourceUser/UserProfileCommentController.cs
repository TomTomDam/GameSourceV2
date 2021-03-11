using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Areas.GameSourceUser
{
    [Route("api/user-profile-comments")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserProfileCommentController : ControllerBase
    {
        private readonly IUserProfileCommentService userProfileCommentService;

        public UserProfileCommentController(IUserProfileCommentService userProfileCommentService)
        {
            this.userProfileCommentService = userProfileCommentService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserProfileComment> result = await userProfileCommentService.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Profile Comment list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Profile Comment list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userProfileCommentService.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Profile Comment.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Profile Comment.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserProfileComment userStatus)
        {
            int rows = await userProfileCommentService.InsertAsync(userStatus);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a User Profile Comment.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new User Profile Comment.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserProfileComment userProfileComment)
        {
            int rows = await userProfileCommentService.UpdateAsync(userProfileComment);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User Profile Comment.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User Profile Comment.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await userProfileCommentService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User Profile Comment.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User Profile Comment.");
        }
    }
}