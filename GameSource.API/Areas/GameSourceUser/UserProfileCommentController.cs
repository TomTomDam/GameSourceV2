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

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Profile Comment list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Profile Comment list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userProfileCommentService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Profile Comment.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Profile Comment.");
        }

        [HttpPost]
        public async Task Insert([FromBody] UserProfileComment userProfileComment)
        {
            await userProfileCommentService.InsertAsync(userProfileComment);
        }

        [HttpPut]
        public async Task Update([FromBody] UserProfileComment userProfileComment)
        {
            await userProfileCommentService.UpdateAsync(userProfileComment);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await userProfileCommentService.DeleteAsync(id);
        }
    }
}