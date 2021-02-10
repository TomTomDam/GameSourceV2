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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserProfile> result = await userProfileService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Profile list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Profile list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userProfileService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Profile.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Profile.");
        }

        [HttpPost]
        public async Task Insert([FromBody] UserProfile userProfile)
        {
            await userProfileService.InsertAsync(userProfile);
        }

        [HttpPost]
        public async Task Update([FromBody] UserProfile userProfile)
        {
            await userProfileService.UpdateAsync(userProfile);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await userProfileService.DeleteAsync(id);
        }
    }
}