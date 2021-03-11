using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Areas.GameSourceUser
{
    [Route("api/user-profiles")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserProfile> result = await userProfileRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Profile list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Profile list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userProfileRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Profile.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Profile.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserProfile userProfile)
        {
            int rows = await userProfileRepository.InsertAsync(userProfile);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new User Profile.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserProfile userProfile)
        {
            int rows = await userProfileRepository.UpdateAsync(userProfile);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User Profile.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await userProfileRepository.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User Profile.");
        }
    }
}