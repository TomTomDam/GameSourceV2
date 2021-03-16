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
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// Gets all UserProfiles
        /// </summary>
        /// <response code="200">Returns a list of UserProfiles</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserProfile> result = await userProfileRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Profile list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Profile list.");
        }

        /// <summary>
        /// Gets a UserProfile by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a UserProfile</response>
        /// <response code="404">Could not find a UserProfile</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userProfileRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Profile.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Profile.");
        }

        /// <summary>
        /// Updates a UserProfile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userProfile"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "displayName": "TomDam",
        ///         "biography": "This is my profile!",
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a UserProfile</response>
        /// <response code="404">Could not find a UserProfile</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserProfile userProfile)
        {
            int rows = await userProfileRepository.UpdateAsync(userProfile);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User Profile.");
        }

        /// <summary>
        /// Deletes a UserProfile
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a UserProfile</response>
        /// <response code="404">Could not find a UserProfile</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            UserProfile userProfile = await userProfileRepository.GetByIDAsync(id);
            if (id == 0 || userProfile == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "UserProfile was not found. Please check the ID.");

            int rows = await userProfileRepository.DeleteAsync(userProfile);
            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User Profile.");
        }
    }
}