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
    [Route("api/user-profiles")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserProfile> result = await userProfileService.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Profile list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Profile list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userProfileService.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Profile.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Profile.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserProfile userProfile)
        {
            int rows = await userProfileService.InsertAsync(userProfile);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new User Profile.");
        }

        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] UserProfile userProfile)
        {
            int rows = await userProfileService.UpdateAsync(userProfile);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User Profile.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await userProfileService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User Profile.");
        }
    }
}